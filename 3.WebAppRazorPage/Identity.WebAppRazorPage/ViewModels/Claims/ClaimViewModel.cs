namespace Identity.WebAppRazorPage.ViewModels.Claims
{
    public class ClaimViewModel
    {
		public string Name { get; set; } = string.Empty;

		public string ClaimType { get; set; } = string.Empty;

		public string ClaimValue { get; set; } = string.Empty;

		public string Issuer { get; set; } = string.Empty;
	}
}
