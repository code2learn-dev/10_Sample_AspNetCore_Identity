namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Roles
{
    public class BaseRolePageModel : PageModel
    {
        protected readonly IMapper _mapper;
        protected IRoleService _roleService;

        protected string IndexPage => "Index";

        public BaseRolePageModel(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }


    }
}
