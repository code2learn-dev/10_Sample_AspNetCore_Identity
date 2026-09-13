using Identity.ApplicationService.Contracts;

namespace Identity.ApplicationService.Courses.Services
{
    public interface ICourseService 
        : IBaseDtoService<
            CourseService,
            Course,
            CourseDtoModel,
            CreateCourseDtoModel,
            UpdateCourseDtoModel,
            DeleteCourseDtoModel>
    {
        Task<ApplicationServiceResult<CourseDtoModel?>> CreateAsync(CreateCourseDtoModel? model, long? teacherId);

        Task<ApplicationServiceResult<CourseDtoModel?>> UpdateAsync(UpdateCourseDtoModel? model, long? teacherId);
    }
}
