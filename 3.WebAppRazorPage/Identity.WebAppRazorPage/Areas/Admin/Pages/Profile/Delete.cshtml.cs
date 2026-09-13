
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Profile
{
    public class DeleteModel : BaseClaimPageModel
    {
        public DeleteModel(IClaimService service, IMapper mapper) : base(service, mapper)
        {
        }

        [BindProperty]
        public DeleteClaimViewModel ClaimModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            var (claimResult, user) = await _claimService.FindClaimByType(id);
            if(!claimResult.IsSuccess)
            {
                claimResult.Errors.MapErrorMessages();
                return RedirectToPage("Claims");
            }

            ClaimModel = _mapper.Map<DeleteClaimViewModel>(claimResult.Result);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var deleteResult = await _claimService.DeleteAsync(_mapper.Map<DeleteClaimDtoModel>(ClaimModel));
            if (!deleteResult.IsSuccess)
            {
                deleteResult.Errors.MapErrorMessages();
                return Page();
            }

            deleteResult.Messages.MappMessages();
            return RedirectToPage("Claims");
        }
    }
}
