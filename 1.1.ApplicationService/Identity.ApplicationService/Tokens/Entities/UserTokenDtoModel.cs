namespace Identity.ApplicationService.Tokens.Entities
{
    public class UserTokenDtoModel : BaseEntityDto
    {
		public string UserId { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public DateTime ExpireDate { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
