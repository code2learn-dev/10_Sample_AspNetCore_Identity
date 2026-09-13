using Identity.Domain.IDentityContent;
using System.Threading.Tasks;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<AcademyUser> _signInManager;

        public LogOutModel(SignInManager<AcademyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Default/Index");
        }
    }
}
