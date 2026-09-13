namespace Identity.WebAppRazorPage.Pages.Account
{
    public abstract class AccountPage : PageModel
    {
        protected readonly IAccountService _accountService;
        protected readonly IMapper _mapper;

        protected string MemberDashboardUrl => "/Member";
        protected string AdminDashboardUrl => "/Admin";

        protected AccountPage(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
    }
}
