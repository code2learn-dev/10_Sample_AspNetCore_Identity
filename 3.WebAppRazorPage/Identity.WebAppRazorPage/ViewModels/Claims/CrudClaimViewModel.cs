namespace Identity.WebAppRazorPage.ViewModels.Claims
{
    public class CrudClaimViewModel
    {
		[Display(Name = "Claim Type")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نوع claim را مشخص کنید")]
		public string ClaimType { get; set; } = string.Empty;

		[Display(Name = "Claim Value")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "مقدار claim را وارد کنید")]
		public string ClaimValue { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;

	}
}
