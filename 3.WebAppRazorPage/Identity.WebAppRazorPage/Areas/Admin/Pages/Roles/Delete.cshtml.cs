
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Roles
{
    public class DeleteModel : BaseRolePageModel
    {
        public DeleteModel(IMapper mapper, IRoleService roleService) : base(mapper, roleService)
        {
        }

        [BindProperty]
        public DeleteRoleViewModel Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            ApplicationServiceResult<DeleteRoleDtoModel?> appResult = await _roleService.FindDeleteRoleByIdAsync(id);
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage(IndexPage);
            }

            Role = _mapper.Map<DeleteRoleViewModel>(appResult.Result);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationServiceResult<RoleDtoModel?> deleteResult = await _roleService.Delete(Role.Id);
            if(deleteResult.IsSuccess)
            {
                deleteResult.Messages.MappMessages();
                return RedirectToPage(IndexPage);
            }

            deleteResult.Errors.MapErrorMessages();
            return Page();
        }
    }
}
