
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class EditModel : BaseClaimPageModel
    {
        public EditModel(IClaimService service, IMapper mapper) : base(service, mapper)
        {
        }

        [BindProperty]
        public CrudClaimViewModel ClaimModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            var (claimResult, user) = await _claimService.FindClaimByType(id);
            if(!claimResult.IsSuccess)
            {
                claimResult.Errors.MapErrorMessages();
                return RedirectToPage("Claims");
            }

            ClaimModel = _mapper.Map<CrudClaimViewModel>(claimResult.Result);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            var editResult = await _claimService.EditClaimAsync(_mapper.Map<CrudClaimDtoModel>(ClaimModel));
            if(!editResult.IsSuccess)
            {
                editResult.Errors.MapErrorMessages();
                return Page();
            }

            editResult.Messages.MappMessages();
            return RedirectToPage("Claims");
        }
    }
}
