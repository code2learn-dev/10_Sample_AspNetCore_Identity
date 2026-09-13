
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class SystemClaimsModel : BaseClaimPageModel
    {
        public SystemClaimsModel(IClaimService service, IMapper mapper) : base(service, mapper)
        {
        }

        public IReadOnlyCollection<ClaimViewModel> ClaimsList { get; set; }

        public IActionResult OnGet()
        {
            ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>> claimsResult = _claimService.GetSystemClaims();
            if(!claimsResult.IsSuccess)
            {
                claimsResult.Errors.MapErrorMessages();
                return RedirectToPage("/Dashboard/Index");
            }

            ClaimsList = _mapper.Map<IReadOnlyCollection<ClaimViewModel>>(claimsResult.Result);
            return Page();
        }
    }
}
