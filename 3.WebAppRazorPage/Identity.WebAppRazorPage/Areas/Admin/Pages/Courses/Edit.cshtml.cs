namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Courses
{
	public class EditModel : BaseAdminCoursePage
	{
		private readonly ICourseService _courseService; 
		private readonly IWebHostEnvironment _webHost;

		public EditModel(
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
		public UpdateCourseViewModel? CourseModel { get; set; } 


		public async Task<IActionResult> OnGetAsync(long? id)
		{
			CourseModel = await PrepareUpdateModelAsync(id, null);
            return CourseModel is null ? RedirectToPage(IndexPage) : Page();
        }


        public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ModelState.MapModelErrors();
				CourseModel = await PrepareUpdateModelAsync(null, CourseModel);
				return Page();
			}

			if (CourseModel is not null && 
				CourseModel.File is not null && 
				CourseModel.File.Length > 0)
			{
                Dictionary<string, string> fileStateResult = 
					await CourseModel.File.EditImageAsync(
														_webHost, 
														ImageDirName, 
														CourseModel.Image, 
														CourseModel.Title);

				CourseModel.Image = !string.IsNullOrEmpty(fileStateResult["filename"])
									? fileStateResult["filename"]
									: CourseModel.Image;
			}

			var appResult = await _courseService.UpdateAsync(_mapper.Map<UpdateCourseDtoModel>(CourseModel), CourseModel.TeacherId);
			if(appResult.IsSuccess)
			{
				appResult.Messages.MappMessages();
				return RedirectToPage(IndexPage);
			}

			appResult.Errors.MapErrorMessages();
			CourseModel = await PrepareUpdateModelAsync(null, CourseModel);
			return Page();
		}
	}
}
