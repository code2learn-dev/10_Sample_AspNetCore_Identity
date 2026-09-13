using Identity.Domain.IDentityContent;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public abstract class BaseAccountProfileModel : PageModel
    {
        protected readonly IAccountService _accountService;
        private readonly RoleManager<AcademyRole> _roleManager; 
        protected readonly IMapper _mapper;

        public BaseAccountProfileModel(
            IAccountService accountService, 
            IMapper mapper, 
            RoleManager<AcademyRole> roleManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        protected IReadOnlyCollection<SelectListItem>? PrepareRolesList()
        {
            List<AcademyRole> roles = _roleManager.Roles.ToList();
            return roles.Select(r => new SelectListItem(r.Name, r.Id)).ToList();
        }
    }
}
