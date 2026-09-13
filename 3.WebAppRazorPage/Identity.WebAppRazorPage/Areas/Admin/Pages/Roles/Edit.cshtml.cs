
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Roles
{
    public class EditModel : BaseRolePageModel
    {
        public EditModel(IMapper mapper, IRoleService roleService) : base(mapper, roleService)
        {
        }

        [BindProperty]
        public UpdateRoleViewModel Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            ApplicationServiceResult<UpdateRoleDtoModel?> appResult = await _roleService.FindUpdateRoleByIdAsync(id);
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage(IndexPage);
            }

            Role = _mapper.Map<UpdateRoleViewModel>(appResult.Result);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            ApplicationServiceResult<RoleDtoModel?> updateResult = 
                await _roleService.Update(_mapper.Map<UpdateRoleDtoModel>(Role));
            if(updateResult.IsSuccess)
            {
                updateResult.Messages.MappMessages();
                return RedirectToPage(IndexPage);
            }

            updateResult.Errors.MapErrorMessages();
            return Page();
        }
    }
}
