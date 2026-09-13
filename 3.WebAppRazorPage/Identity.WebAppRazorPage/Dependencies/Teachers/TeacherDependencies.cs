namespace Identity.WebAppRazorPage.Dependencies.Teachers
{
    public static class TeacherDependencies
    {
        public static IServiceCollection ConfigureTeacherServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherResponse, TeacherResponse>();
            services.AddScoped<ITeacherMessageMaker, TeacherMessageMaker>();

            return services;
        }
    }
}
