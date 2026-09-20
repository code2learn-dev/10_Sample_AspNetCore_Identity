namespace Identity.ApplicationService.Tokens.Entities
{
    public class RefreshTokenDtoModel : BaseEntityDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
