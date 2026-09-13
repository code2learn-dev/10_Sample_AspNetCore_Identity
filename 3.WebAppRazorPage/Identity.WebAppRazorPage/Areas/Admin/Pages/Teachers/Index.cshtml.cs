using Microsoft.AspNetCore.Authorization;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Teachers
{
    public class IndexModel : BasePage
    {
        private readonly ITeacherService _teacherService;
        private readonly IAuthorizationService _authorizationService;

        public IndexModel(IMapper mapper, ITeacherService teacherService, IAuthorizationService authorizationService) : base(mapper)
        {
            _teacherService = teacherService;
            _authorizationService = authorizationService;
        }

        public IReadOnlyCollection<TeacherViewModel> Teachers { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var appResult = await _teacherService.GetAllEntityDtosAsync();
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage(IndexPage);
            }

            Teachers = _mapper.Map<IReadOnlyCollection<TeacherViewModel>>(appResult.Result);
            AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, Teachers, "teacherslist");
            if(!result.Succeeded)
            {
                MessageHelperExtensions.SetErrorMessage("دسترسی شما به این قسمت تعریف نشده است");
                return RedirectToPage("/Dashboard/Index");
            }

            return Page();
        }
    }
}
