namespace Identity.ApplicationService.Claims.Entities
{
    public class ClaimDtoModel
    {
        public string Name { get; set; } = string.Empty;

        public string ClaimType { get; set; } = string.Empty;

        public string ClaimValue { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;
    }
}
