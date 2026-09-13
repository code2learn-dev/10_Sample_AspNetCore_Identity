using Identity.Domain.IDentityContent;
using Identity.WebAppRazorPage.Dependencies.Identities.CustomPolicies;
using Identity.WebAppRazorPage.Dependencies.Identities.ResourcePolicies;
using Identity.WebAppRazorPage.Dependencies.Identities.ViewPolicies;
using Microsoft.AspNetCore.Authorization;

namespace Identity.WebAppRazorPage.Dependencies.Identities
{
    public static class IdentityExtensions
    {
        public static IServiceCollection ConfigIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AcademyUser, AcademyRole>()
                .AddEntityFrameworkStores<AcademyDbContext>()
                .AddDefaultTokenProviders()
                .AddPasswordValidator<AcademyPasswordValidator>()
                .AddErrorDescriber<CustomIdentityErrors>();

            return services;
        }

        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // password configs
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;


                options.User.RequireUniqueEmail = true;


                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;


                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                 
            });

            return services;
        }


        public static IServiceCollection ConfigureIdentityCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/";
                options.Cookie.MaxAge = TimeSpan.FromDays(7);
                options.AccessDeniedPath = "/Manage/NotFound";
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

        
        public static IServiceCollection RegisterCustomClaims(this IServiceCollection services)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<AcademyUser>, PhoneNumberClaim>();

            return services;
        }


        public static IServiceCollection ConfigurePolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                // role authentication 
                .AddPolicy("amdinpolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("admin");
                })
                // role authentication 
                .AddPolicy("memberpolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("member");
                })
                // claim authentication 
				//.AddPolicy("phonenumberpolicy", policyBuilder =>
				//{
				//	policyBuilder.RequireAuthenticatedUser();
				//	policyBuilder.RequireClaim("phonenumber");
				//})
                
                // add custom policy
                .AddPolicy("nc", policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new NationalCodePolicyRequirement());
                })
                //  add a custom policy that it functions by age
                .AddPolicy("age", policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new AgePolicyRequirement(21));
                })
                
                // add resource policies
                .AddPolicy("claimslist", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.Requirements.Add(new ClaimListPolicyRequirement());
                })
                .AddPolicy("teacherslist", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.Requirements.Add(new TeacherListPolicyRequirement());
                })

                // add view resource policies
                .AddPolicy("userslist", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.Requirements.Add(new UsersListPolicyRequirement());
                });


            return services;
        }


        public static IServiceCollection InjectCustomPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, NationaCodePolicyHandler>();
            services.AddSingleton<IAuthorizationHandler, AgePolicyHandler>();

            // register resource policy services
            services.AddSingleton<IAuthorizationHandler, ClaimListPolicyHandler>();
            // teachers list resource policy
            services.AddSingleton<IAuthorizationHandler, TeacherListPolicyHandler>();
            // users list view policy
            services.AddSingleton<IAuthorizationHandler, UsersListPolicyHandler>();

            return services;
        }
    }
}
