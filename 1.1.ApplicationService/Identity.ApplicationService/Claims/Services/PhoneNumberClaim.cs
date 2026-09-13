using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Identity.ApplicationService.Claims.Services
{
    public class PhoneNumberClaim : UserClaimsPrincipalFactory<AcademyUser, AcademyRole>
    {
        public PhoneNumberClaim(
            UserManager<AcademyUser> userManager, 
            RoleManager<AcademyRole> roleManager, 
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AcademyUser user)
        {
            ClaimsIdentity claimsIdentity = await base.GenerateClaimsAsync(user);
            claimsIdentity.AddClaim(new Claim("phonenumber", user.PhoneNumber ?? "09356030377"));
            return claimsIdentity;
        }
    }
}
