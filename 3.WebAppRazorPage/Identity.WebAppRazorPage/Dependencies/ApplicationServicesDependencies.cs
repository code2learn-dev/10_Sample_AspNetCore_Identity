using Identity.WebAppRazorPage.Dependencies.Accounts;
using Identity.WebAppRazorPage.Dependencies.Claims;
using Identity.WebAppRazorPage.Dependencies.Identities;
using Identity.WebAppRazorPage.Dependencies.Roles;
using Identity.WebAppRazorPage.Dependencies.Users;

namespace Identity.WebAppRazorPage.Dependencies
{
    public static class ApplicationServicesDependencies
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // system depedencies
            // add automapper service
            builder.Services.AddAutoMapper(a => a.AddProfile<DtoMappingProfile>());
            builder.Services.AddAutoMapper(a => a.AddProfile<ViewModelMappingProfiles>());

            // add fluent vlaidation
            builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();


            // public dependencies
            builder.Services.AddScoped<IModelValidator, ModelValidator>();

            // apply authorize filter to all routes 

            // razor page service
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Admin", "/", "amdinpolicy");
                options.Conventions.AuthorizeAreaFolder("Member", "/", "memberpolicy");

                // authorize area folder by custom policy
                //options.Conventions.AuthorizeAreaFolder("Admin", "/Users", "nc");

                // authorize claims with policy
                //options.Conventions.AuthorizeAreaPage("Admin", "/Profile/Claims", "nc");
                //options.Conventions.AuthorizeAreaPage("Admin", "/Profile/Add", "nc");
                //options.Conventions.AuthorizeAreaPage("Admin", "/Profile/Edit", "nc");
                //options.Conventions.AuthorizeAreaPage("Admin", "/Profile/Delete", "nc");

                // apply age policy to the teachers route
                //options.Conventions.AuthorizeAreaFolder("Admin", "/Teachers", "age"); 


                // when apply the authorization to all paths
                //options.Conventions.AuthorizeFolder("/");

                // assign allow path
                //options.Conventions.AllowAnonymousToFolder("/Account");
                //options.Conventions.AllowAnonymousToFolder("/Manage");
            });

            // inject common service
            builder.Services.InjectCommonServices();


            // inject entites services
            builder.Services
                // inject cateogires services
                .ConfigureCategoriesServices()
                // inject courses services
                .ConfigureCoursesServices()
                // inject teacher services
                .ConfigureTeacherServices()
                // inject user services
                .ConfigureUserServices()
                // inject role services
                .ConfigureRolesServices()
                // inject account service
                .ConfigureAccountServices()
                // inject claim services
                .ConfigureClaimsService();

            // config identity services
            builder.Services
                // add identity service
                .ConfigIdentityServices()
                // config password/username and signin options
                .ConfigureIdentityOptions()
                // config login and access denied path            
                .ConfigureIdentityCookie()
                // config cookie lifetime            
                .ConfigureIdentityTokenProvider()
                // register custom claims
                .RegisterCustomClaims()
                // config policies
                .ConfigurePolicies()
                // inject custom policies
                .InjectCustomPolicies();

            return builder;

        }

        public static WebApplication ConfigurePipeline(this WebApplicationBuilder builder)
        {
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<NotFoundMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages(); 

            return app;
        }
    }
}
