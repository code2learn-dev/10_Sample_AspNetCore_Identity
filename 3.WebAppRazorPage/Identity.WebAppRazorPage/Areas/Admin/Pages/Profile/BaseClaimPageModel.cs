using Identity.ApplicationService.Claims.Services;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public abstract class BaseClaimPageModel : PageModel
    {
        protected readonly IClaimService _claimService;
        protected readonly IMapper _mapper;

        public BaseClaimPageModel(IClaimService service, IMapper mapper)
        {
            _claimService = service;
            _mapper = mapper;
        }
    }
}
