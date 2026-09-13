
using Identity.ApplicationService.Teachers.Entities;
using Identity.WebAppRazorPage.ViewModels.Teachers;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Teachers
{
    public class EditModel : BasePage
    {
        private readonly ITeacherService _teacherService;
        private IWebHostEnvironment _webHost;

        public EditModel(
            IMapper mapper, 
            ITeacherService teacherService, 
            IWebHostEnvironment webHost) : base(mapper)
        {
            _teacherService = teacherService;
            _webHost = webHost;
        }

        [BindProperty]
        public UpdateTeacherViewModel TeacherModel { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            var appResult = await _teacherService.FindByIdUpdateEntityDtoAsync(id);
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage(IndexPage);
            }

            TeacherModel = _mapper.Map<UpdateTeacherViewModel>(appResult.Result);
            TeacherModel.TeacherImageUrl = $"https://localhost:7286/assets/images/Teachers/{TeacherModel.Image}";
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            if(TeacherModel.File is not null)
            {
                Dictionary<string, string> fileState = await TeacherModel.File.EditImageAsync(
                                                                                    _webHost,
                                                                                    "Teachers",
                                                                                    TeacherModel.Image,
                                                                                    $"{TeacherModel.FirstName} {TeacherModel.LastName}");
                if (string.IsNullOrEmpty(fileState["filename"]))
                {
                    MessageHelperExtensions.SetErrorMessage(fileState["errpr"]);
                    return Page();
                }
                TeacherModel.Image = fileState["filename"];
            }

            var appResult = await _teacherService.UpdateEntityDtoAsync(_mapper.Map<UpdateTeacherDtoModel>(TeacherModel));
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
