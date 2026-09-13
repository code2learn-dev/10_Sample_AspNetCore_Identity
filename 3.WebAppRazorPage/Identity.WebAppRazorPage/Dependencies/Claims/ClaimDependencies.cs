using Identity.ApplicationService.Claims.Response;
using Identity.ApplicationService.Claims.Validators;
using Identity.Domain.IDentityContent;

namespace Identity.WebAppRazorPage.Dependencies.Claims
{
    public static class ClaimDependencies
    {
        public static IServiceCollection ConfigureClaimsService(this IServiceCollection services)
        {
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IClaimMessageMaker, ClaimMessageMaker>();
            services.AddScoped<IClaimValidator, ClaimValidator>();

            // config custom cliams
            // it'll create immediately after user logined
            //services.AddScoped<IUserClaimsPrincipalFactory<AcademyUser>, PhoneNumberClaim>();

            return services;
        }
    }
}
