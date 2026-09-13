using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Courses
{
	public abstract class BaseAdminCoursePage : BasePage
	{
		private readonly ICourseService _courseService;
		private readonly ICategoryService _categoryService;
		private readonly ITeacherService _teacherService;

		protected string ImageDirName => "Courses";

		protected BaseAdminCoursePage(
			IMapper mapper,
			ICategoryService categoryService,
			ITeacherService teacherService,
			ICourseService courseService) : base(mapper)
		{
			_categoryService = categoryService;
			_teacherService = teacherService;
			_courseService = courseService;
		}

		protected virtual async Task<UpdateCourseViewModel?> PrepareUpdateModelAsync(long? id, UpdateCourseViewModel? model)
		{
			if(id is not null and > 0)
			{
				var appResult = await _courseService.FindByIdUpdateEntityDtoAsync(id);
				if(!appResult.IsSuccess)
				{
					appResult.Errors.MapErrorMessages();
					return null;
				}

				model = _mapper.Map<UpdateCourseViewModel>(appResult.Result);
			}

			model?.CategorySelectListItem = await GetCategorySelectListItemAsync();
			model?.TeacherSelectListItem = await GetTeachersSelectListItemAsync();
			model?.CourseImageUrl = $"https://localhost:7286/assets/images/courses/{model.Image}";
			return model;
		}

		protected virtual async Task<CreateCourseViewModel> PrepareCreateModelAsync(CreateCourseViewModel? model = null)
		{
			model ??= new();

			model.CategorySelectListItem = await GetCategorySelectListItemAsync();
			model.TeacherSelectListItem = await GetTeachersSelectListItemAsync();
			return model;
		}

		protected virtual async Task<DeleteCourseViewModel?> PrepareDeleteCourseModel(long? id, DeleteCourseViewModel? model)
		{
			if (id is not null and > 0 && model is null)
			{
				var appResult = await _courseService.FindByIdDeleteEntityDtoAsync(id);
				if (!appResult.IsSuccess)
				{
					appResult.Errors.MapErrorMessages();
					return null;
				}

				model = _mapper.Map<DeleteCourseViewModel>(appResult.Result);
			}

			model?.CategorySelectListItem = await GetCategorySelectListItemAsync();
			model?.TeacherSelectListItem = await GetTeachersSelectListItemAsync();
			model?.CourseImageUrl = $"https://localhost:7286/assets/images/courses/{model.Image}";

			return model;
		}

		protected virtual async Task<IReadOnlyCollection<SelectListItem>?> GetCategorySelectListItemAsync()
		{
			var appResult = await _categoryService.GetAllEntityDtosAsync("خطا در نمایش دسته های آموزشی");
			if (!appResult.IsSuccess)
			{
				appResult.Errors.MapErrorMessages();
				return null;
			}

			IReadOnlyCollection<CategoryViewModel> categories = _mapper.Map<IReadOnlyCollection<CategoryViewModel>>(appResult.Result);
			return [.. categories.Select(ca => new SelectListItem(ca.Title, ca.Id.ToString()))];
		}

		protected virtual async Task<IReadOnlyCollection<SelectListItem>?> GetTeachersSelectListItemAsync()
		{
			var appResult = await _teacherService.GetAllEntityDtosAsync("خطا در نمایش لیست مدرسین");
			if (!appResult.IsSuccess)
			{
				appResult.Errors.MapErrorMessages();
				return null;
			}

			IReadOnlyCollection<TeacherViewModel> teachers = _mapper.Map<IReadOnlyCollection<TeacherViewModel>>(appResult.Result);
			return [.. teachers.Select(ta => new SelectListItem($"{ta.FirstName} {ta.LastName}", ta.Id.ToString()))];
		}
	}
}
