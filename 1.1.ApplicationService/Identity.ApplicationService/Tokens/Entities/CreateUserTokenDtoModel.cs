using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Identity.ApplicationService.Tokens.Entities
{
    public class CreateUserTokenDtoModel : BaseEntityDto
    {
		public string UserId { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public DateTime ExpireDate { get; set; }
		public bool IsActive { get; set; } = true;
        public TokenValidatedContext? Context { get; set; } 
		public Dictionary<string, string>? JwtSections { get; set; }
        public JwtSectionConfiguration? JwtConfiguration { get; set; }
    }
}
