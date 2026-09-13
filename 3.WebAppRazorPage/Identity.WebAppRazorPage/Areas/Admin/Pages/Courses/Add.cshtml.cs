namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Courses
{
	public class AddModel : BaseAdminCoursePage
	{
		private readonly ICourseService _courseService;
		private readonly IWebHostEnvironment _webHost;

		public AddModel(
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
		public CreateCourseViewModel CourseModel { get; set; }

		public async Task<IActionResult> OnGet()
		{
			CourseModel = await PrepareCreateModelAsync();
			return Page();
		}


		public async Task<IActionResult> OnPostAsync(IFormFile? File)
		{
			if (!ModelState.IsValid)
			{
				ModelState.MapModelErrors();
				CourseModel = await PrepareCreateModelAsync(CourseModel);
				return Page();
			}

			if (CourseModel.File is not null && CourseModel.File.Length > 0)
			{
				Dictionary<string, string> fileState = await CourseModel.File.UploadImgeAsync(_webHost, "courses", CourseModel.Title);
				if (string.IsNullOrEmpty(fileState["filename"]))
				{
					MessageHelperExtensions.SetErrorMessage(fileState["error"]);
					CourseModel.CategorySelectListItem = await GetCategorySelectListItemAsync();
					return Page();
				}

				CourseModel.Image = fileState["filename"];
			}

			var appResult = await _courseService.CreateAsync(_mapper.Map<CreateCourseDtoModel>(CourseModel), CourseModel.TeacherId);
			if (appResult.IsSuccess)
			{
				appResult.Messages.MappMessages();
				return RedirectToPage("Index");
			}
			 
			appResult.Errors.MapErrorMessages();
			CourseModel = await PrepareCreateModelAsync(CourseModel);
			return Page();
		}
	}
}
