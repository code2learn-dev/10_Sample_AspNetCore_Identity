
using System.Threading.Tasks;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class AddModel : BaseClaimPageModel
    {
        public AddModel(IClaimService service, IMapper mapper) : base(service, mapper)
        {
        }

        [BindProperty]
        public CrudClaimViewModel ClaimModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ApplicationServiceResult<string?> userIdResult = await _claimService.GetCurrentUserIdAsync();
            if(!userIdResult.IsSuccess || string.IsNullOrEmpty(userIdResult.Result))
            {
                userIdResult.Errors.MapErrorMessages();
                return RedirectToAction("Claims");
            };

            ClaimModel = new()
            {
                UserId = userIdResult.Result
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            var addResult = await _claimService.AddAsync(_mapper.Map<CrudClaimDtoModel>(ClaimModel));
            if(!addResult.IsSuccess)
            {
                addResult.Errors.MapErrorMessages();
                return Page();
            }

            addResult.Messages.MappMessages();
            return RedirectToPage("Claims");
        }
    }
}
