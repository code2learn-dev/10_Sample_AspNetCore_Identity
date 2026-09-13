using Identity.Domain.IDentityContent;

namespace Identity.WebAppRazorPage.Areas.Member.Pages.Profile
{
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<AcademyUser> _signInManager;

        public LogOutModel(SignInManager<AcademyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Default/Index");
        }
    }
}
