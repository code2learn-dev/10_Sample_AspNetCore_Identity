namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Courses
{
	public class DeleteModel : BaseAdminCoursePage
	{
		private readonly ICourseService _courseService;
		private readonly IWebHostEnvironment _webHost; 

		public DeleteModel(
			IMapper mapper,
			ICategoryService categoryService,
			ICourseService courseService,
			ITeacherService teacherService,
			IWebHostEnvironment webHost)
			: base(mapper, categoryService, teacherService, courseService)
		{
			_courseService = courseService;
			_webHost = webHost;
		}

		[BindProperty]
		public DeleteCourseViewModel CourseModel { get; set; }

		public async Task<IActionResult> OnGetAsync(long? id)
		{
			DeleteCourseViewModel? deleteModel = await PrepareDeleteCourseModel(id, null);
			if (deleteModel is null) return RedirectToPage(IndexPage);

			CourseModel = deleteModel;
			return Page();
		}


		public async Task<IActionResult> OnPostAsync()
		{
			CourseModel.Image.DeleteImage(_webHost, "Courses");

			var appResult = await _courseService.DeleteEntityDtoAsync(CourseModel.Id);
			if (appResult.IsSuccess)
			{
				appResult.Messages.MappMessages();
				return RedirectToPage(IndexPage);
			}

			appResult.Errors.MapErrorMessages();
            CourseModel = await PrepareDeleteCourseModel(null, CourseModel) ?? new();
			return Page();
		}
	}
}
