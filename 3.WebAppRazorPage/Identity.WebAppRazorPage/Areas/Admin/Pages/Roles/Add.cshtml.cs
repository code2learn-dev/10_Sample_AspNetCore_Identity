
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Roles
{
    public class AddModel : BaseRolePageModel
    {
        public AddModel(IMapper mapper, IRoleService roleService) : base(mapper, roleService)
        {
        }

        [BindProperty]
        public CreateRoleViewModel Role { get; set; }

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

            ApplicationServiceResult<RoleDtoModel?> appResult = await _roleService.CreateAsync(_mapper.Map<CreateRoleDtoModel>(Role));
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
