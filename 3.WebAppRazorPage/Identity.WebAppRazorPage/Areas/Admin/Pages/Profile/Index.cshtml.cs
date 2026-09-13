using Identity.Domain.IDentityContent;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class IndexModel : BaseAccountProfileModel
    {  
        public IndexModel(
            IAccountService accountService, 
            RoleManager<AcademyRole> roleManager,
            IMapper mapper) : base(accountService, mapper, roleManager)
        { 
        }

        public AccountProfileViewModel AccountProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationServiceResult<AccountProfileDtoModel?> profileResult = await _accountService.GetAccountProfileInfoAsync();
            if(!profileResult.IsSuccess)
            {
                profileResult.Errors.MapErrorMessages();
                return RedirectToPage("/Dashboard/Index");
            }

            AccountProfile = _mapper.Map<AccountProfileViewModel>(profileResult.Result);
            AccountProfile.RolesSelectListItem = PrepareRolesList();
            AccountProfile.Image = $"{AccountProfile.ImageUrl}/{AccountProfile.Image}";
            return Page();
        }
    }
}
