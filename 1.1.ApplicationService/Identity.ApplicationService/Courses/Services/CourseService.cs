using Identity.ApplicationService.Teachers.Entities;
using Identity.ApplicationService.Teachers.Services;

namespace Identity.ApplicationService.Courses.Services
{
	public class CourseService
		: BaseDtoService<
			CourseService,
			Course,
			CourseDtoModel,
			CreateCourseDtoModel,
			UpdateCourseDtoModel,
			DeleteCourseDtoModel>,

		ICourseService
	{
		private readonly ICourseRepository _courseRepository;
		private readonly ITeacherService _teacherService;

		public CourseService(
			ICourseRepository repository,
			ICourseResponse serviceResponse,
			IMapper mapper,
			ICourseMessageMaker messageMaker,
			ILogger<CourseService> logger,
			IModelValidator modelValidator,
			ITeacherService teacherService)

			: base(repository, serviceResponse, mapper, messageMaker, logger, modelValidator)
		{
			_courseRepository = repository;
			_teacherService = teacherService;
		}

        public override async Task<ApplicationServiceResult<UpdateCourseDtoModel?>> FindByIdUpdateEntityDtoAsync(long? id)
        {
			ApplicationServiceResult<UpdateCourseDtoModel?> appResult = _serviceResponse.GetEntityResultResponse<UpdateCourseDtoModel>();

            if(id is null or <= 0)
			{
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_messageMaker.Message);
				return appResult;
			}

            Course? course = await _courseRepository.FindCourseForCrudAsync(id ?? 0);
			if(course is null)
			{
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_messageMaker.Message);
				return appResult;
			}

			UpdateCourseDtoModel updateModel = new()
			{
				Id = course.Id,
				Title = course.Title,
				Description = course.Description,
				Price = course.Price,
				Image = course.Image,
				CategoryId = course.CategoryId,
				TeacherId = course.Teachers.FirstOrDefault()?.Id ?? 0
			};
			appResult.AddResult(updateModel);
			return appResult;
        }

        public override async Task<ApplicationServiceResult<DeleteCourseDtoModel?>> FindByIdDeleteEntityDtoAsync(long? id)
        {
			ApplicationServiceResult<DeleteCourseDtoModel?> appResult = _serviceResponse.GetEntityResultResponse<DeleteCourseDtoModel>();

			if(id is null or <= 0)
			{
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_messageMaker.Message);
				return appResult;
			}

            Course? course = await _courseRepository.FindCourseForCrudAsync(id ?? 0);
			if(course is null)
			{
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_messageMaker.Message);
				return appResult;
			}

			DeleteCourseDtoModel deleteModel = new()
			{
				Id = course.Id,
				Title = course.Title,
				Price = course.Price,
				Image = course.Image,
				CategoryId = course.CategoryId,
				TeacherId = course.Teachers.FirstOrDefault()?.Id ?? 0
			};
			appResult.AddResult(deleteModel);
			return appResult;
        }

		public async Task<ApplicationServiceResult<CourseDtoModel?>> CreateAsync(CreateCourseDtoModel? model, long? teacherId)
		{
			ApplicationServiceResult<CourseDtoModel?> appResult = await ValidateModelAsync(model);
			if (!appResult.IsSuccess || model is null) return appResult;

			ApplicationServiceResult<TeacherDtoModel?> teacherAppResult = await _teacherService.FindByIdEntityDtoAsync(model.TeacherId);
			if (!teacherAppResult.IsSuccess)
			{
				appResult.AddErrorsList([.. teacherAppResult.Errors]);
				appResult.AddResult(default);
				return appResult;
			}

			Course? course = await _courseRepository.CreateAsync(_mapper.Map<Course>(model), model.TeacherId);
			return SetAppResult(appResult, course);
		}

		public async Task<ApplicationServiceResult<CourseDtoModel?>> UpdateAsync(UpdateCourseDtoModel? model, long? teacherId)
        {
            ApplicationServiceResult<CourseDtoModel?> appResult = await ValidateModelAsync(model);
            if (!appResult.IsSuccess || model is null) return appResult; 

            Course? course = await _courseRepository.UpdateAsync(_mapper.Map<Course>(model), model.TeacherId);
            return SetAppResult(appResult, course, Crud.update);
        }

        private ApplicationServiceResult<CourseDtoModel?> SetAppResult(
			ApplicationServiceResult<CourseDtoModel?> appResult, 
			Course? course,
			Crud crudType = Crud.create)
        {
            if (course is not null)
            {
                appResult.AddResult(_mapper.Map<CourseDtoModel>(course));
                _messageMaker.SetMessage(crudType, HttpStatusCode.OK);
                appResult.AddMessage(_messageMaker.Message);
                return appResult;
            }

            appResult.AddResult(default);
            _messageMaker.SetMessage(crudType, HttpStatusCode.BadRequest);
            appResult.AddError(_messageMaker.Message);
            return appResult;
        }
    }
}
