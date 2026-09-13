namespace Identity.WebAppRazorPage.Pages.Account
{
    public class LoginModel : AccountPage
    {
        public LoginModel(IAccountService accountService, IMapper mapper) : base(accountService, mapper)
        {
        }

        [BindProperty]
        public LoginViewModel Login { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            ApplicationServiceResult<AccountDtoModel?> loginResult = 
                await _accountService.LoginAccountAsync(_mapper.Map<LoginDtoModel>(Login));
            if(!loginResult.IsSuccess)
            {
                loginResult.Errors.MapErrorMessages();
                return Page();
            }

            
            return !string.IsNullOrEmpty(Login.ReturnUrl) &&
                    Url.IsLocalUrl(Login.ReturnUrl)
                    ? Redirect(Login.ReturnUrl) 
                    : Redirect(MemberDashboardUrl);
        }
    }
}
