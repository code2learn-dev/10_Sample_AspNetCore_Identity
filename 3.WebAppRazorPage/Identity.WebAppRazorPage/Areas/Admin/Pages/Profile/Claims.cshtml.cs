using Microsoft.AspNetCore.Authorization;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class ClaimsModel : BaseClaimPageModel
    {
        private readonly IAuthorizationService _authorizationService;

        public ClaimsModel(
            IClaimService service, 
            IMapper mapper, 
            IAuthorizationService authorizationService) 
            : base(service, mapper)
        {
            _authorizationService = authorizationService;
        }

        public IReadOnlyCollection<ClaimViewModel> ClaimsList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>> claimResult = await _claimService.GetAllUserClaims();
            if(!claimResult.IsSuccess)
            {
                claimResult.Errors.MapErrorMessages();
                return RedirectToPage("/Dashboard/Index");
            }


            ClaimsList = _mapper.Map<IReadOnlyCollection<ClaimViewModel>>(claimResult.Result);
            AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, ClaimsList, "claimslist");
            
            if(!result.Succeeded)
            {
                MessageHelperExtensions.SetErrorMessage("دسترسی شما به این قسمت تعریف نشده است");
                return RedirectToPage("/Dashboard/Index");
			}

            return Page();
        }
    }
}
