namespace Identity.ApplicationService.Tokens.Entities
{
    public class JwtSectionConfiguration
    {
        public string Issuer { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
