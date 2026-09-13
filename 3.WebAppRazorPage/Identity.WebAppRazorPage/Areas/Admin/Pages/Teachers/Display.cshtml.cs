namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Teachers
{
    public class DisplayModel : BasePage
    {
        private readonly ITeacherService _teacherService;

        public DisplayModel(IMapper mapper, ITeacherService teacherService) : base(mapper)
        {
            _teacherService = teacherService;
        }

		public DeleteTeacherViewModel TeacherModel { get; set; }

		public async Task<IActionResult> OnGetAsync(long? id)
		{
			var appResult = await _teacherService.FindByIdDeleteEntityDtoAsync(id);
			if (!appResult.IsSuccess)
			{
				appResult.Errors.MapErrorMessages();
				return RedirectToPage(IndexPage);
			}

			TeacherModel = _mapper.Map<DeleteTeacherViewModel>(appResult.Result);
			TeacherModel.TeacherImageUrl = $"https://localhost:7286/assets/images/Teachers/{TeacherModel.Image}";
			return Page();
		}
	}
}
