namespace _10_Identity.WebApiApp.Dependencies.Identites
{
    public static class IdentityConfigurations
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AcademyUser>()
                .AddRoles<AcademyRole>()
                .AddEntityFrameworkStores<AcademyDbContext>()
                .AddDefaultTokenProviders()
                .AddPasswordValidator<AcademyPasswordValidation>()
                .AddErrorDescriber<AcademyIdentityErrors>();
            return services;
        }


        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(20);
            });
            return services;
        }


        public static IServiceCollection ConfigureIdentityTokenProvider(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(7);
            });

            return services;
        }
    }
}
