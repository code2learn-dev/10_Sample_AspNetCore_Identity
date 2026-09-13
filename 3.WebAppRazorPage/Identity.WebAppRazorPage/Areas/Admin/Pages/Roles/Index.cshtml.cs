namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Roles
{
    public class IndexModel : BaseRolePageModel
    {
        public IndexModel(IMapper mapper, IRoleService roleService) : base(mapper, roleService)
        {
        }

        public IReadOnlyCollection<RoleViewModel> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationServiceResult<IReadOnlyCollection<RoleDtoModel>> appResult = _roleService.ReadAllRoles();
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToAction("/Admin/Dashboard");
            }

            Roles = _mapper.Map<IReadOnlyCollection<RoleViewModel>>(appResult.Result);
            return Page();
        }
    }
}
