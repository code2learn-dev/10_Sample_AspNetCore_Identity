using Identity.ApplicationService.Account.Validators;
using Identity.ApplicationService.Tokens.Services;
using Identity.Repository.Users;

namespace _10_Identity.WebApiApp.Dependencies.Tokens
{
    public static class TokenDependencies
    {
        public static IServiceCollection ConfigureTokenServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IModelValidator, ModelValidator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountModelValidator, AccountModelValidator>();
            return services;
        }
    }
}
