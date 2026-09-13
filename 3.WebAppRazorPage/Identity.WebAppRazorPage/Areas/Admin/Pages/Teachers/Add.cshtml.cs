namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Teachers
{
	public class AddModel : BasePage
	{
		private readonly ITeacherService _teacherService;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public AddModel(
			IMapper mapper,
			ITeacherService teacherService,
			IWebHostEnvironment webHostEnvironment) : base(mapper)
		{
			_teacherService = teacherService;
			_webHostEnvironment = webHostEnvironment;
		}

		[BindProperty]
		public CreateTeacherViewModel TeacherModel { get; set; }

		public void OnGet()
		{
		}


		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ModelState.MapModelErrors();
				return Page();
			}

			Dictionary<string, string> fileState = await TeacherModel.File.UploadImgeAsync(
														_webHostEnvironment,
														"Teachers",
														$"{TeacherModel.FirstName}_{TeacherModel.LastName}");
			if (string.IsNullOrEmpty(fileState["filename"]))
			{
				MessageHelperExtensions.SetErrorMessage(fileState["error"]);
				return Page();
			}
			else
				TeacherModel.Image = fileState["filename"];

			var appResult = await _teacherService.AddEntityDtoAsync(_mapper.Map<CreateTeacherDtoModel>(TeacherModel));
			if(appResult.IsSuccess)
			{
				appResult.Messages.MappMessages();
				return RedirectToPage(IndexPage);
			}

			appResult.Errors.MapErrorMessages();
			return Page();
		}
	}
}
