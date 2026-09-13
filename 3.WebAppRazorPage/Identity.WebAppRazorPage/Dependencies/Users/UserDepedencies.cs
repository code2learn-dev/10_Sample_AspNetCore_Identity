namespace Identity.WebAppRazorPage.Dependencies.Users
{
    public static class UserDepedencies
    {
        public static IServiceCollection ConfigureUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMessageMaker, UserMessageMaker>();
            services.AddScoped<IUserResponse, UserResponse>();
            services.AddScoped<IUserModelValidator, UserModelValidator>();

            return services;
        }
    }
}
