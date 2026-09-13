using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Users
{
    public abstract class BaseUserPageModel : PageModel
    {
        protected readonly IUserService _userService;
        protected readonly IMapper _mapper;
        protected readonly IRoleService _roleService;

        protected string IndexPage => "Index";

        protected string UserImagesUrl => "https://localhost:7286/assets/images/users";

        protected string UserImageDirName => "users";

        public BaseUserPageModel(
            IUserService userService,
            IMapper mapper,
            IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        protected virtual IReadOnlyCollection<SelectListItem>? PrepareRolesList()
        {
            ApplicationServiceResult<IReadOnlyCollection<RoleDtoModel>> rolesResult = _roleService.ReadAllRoles();
            if(!rolesResult.IsSuccess)
            {
                rolesResult.Errors.MapErrorMessages();
                return [];
            }

            IReadOnlyCollection<SelectListItem> rolesList =
                            rolesResult.Result?.Select(a => new SelectListItem(a.Name, a.Id)).ToList() ?? [];
            return rolesList;
        }
    }
}
