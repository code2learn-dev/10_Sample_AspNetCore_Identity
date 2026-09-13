namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Teachers
{
    public class DeleteModel : BasePage
    {
        private readonly ITeacherService _teacherService;
        private readonly IWebHostEnvironment _webHost;

        public DeleteModel(
            IMapper mapper,
            ITeacherService teacherService,
            IWebHostEnvironment webHost) : base(mapper)
        {
            _teacherService = teacherService;
            _webHost = webHost;
        }

        [BindProperty]
        public DeleteTeacherViewModel TeacherModel { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            var appResult = await _teacherService.FindByIdDeleteEntityDtoAsync(id);
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage(IndexPage);
            }

            TeacherModel = _mapper.Map<DeleteTeacherViewModel>(appResult.Result);
            TeacherModel.TeacherImageUrl = $"https://localhost:7286/assets/images/Teachers/{TeacherModel.Image}";
			return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            TeacherModel.Image.DeleteImage(_webHost, "Teachers");
            var appResult = await _teacherService.DeleteEntityDtoAsync(TeacherModel.Id);
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
