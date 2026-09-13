namespace Identity.ApplicationService.Claims.Entities
{
    public class DeleteClaimDtoModel
    {
		public string ClaimType { get; set; } = string.Empty;

		public string ClaimValue { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;
	}
}
