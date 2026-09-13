
namespace Identity.WebAppRazorPage.Dependencies.Accounts
{
    public static class AccountDependencies
    {
        public static IServiceCollection ConfigureAccountServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountModelValidator, AccountModelValidator>();
            services.AddScoped<IAccountMessageMaker, AccountMessageMaker>();
            services.AddScoped<IAccountResponse, AccountResponse>();
            return services;
        }
    }
}
