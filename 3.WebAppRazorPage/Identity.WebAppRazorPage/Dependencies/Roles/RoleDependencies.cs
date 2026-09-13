namespace Identity.WebAppRazorPage.Dependencies.Roles
{
    public static class RoleDependencies
    {
        public static IServiceCollection ConfigureRolesServices(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleMessageMaker, RoleMessageMaker>();
            services.AddScoped<IRoleResponse, RoleResponse>();
            services.AddScoped<IRoleModelValidator, RoleModelValidator>();

            return services;
        }
    }
}
