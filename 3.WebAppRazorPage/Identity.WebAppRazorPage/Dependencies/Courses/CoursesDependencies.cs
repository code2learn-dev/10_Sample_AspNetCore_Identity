namespace Identity.WebAppRazorPage.Dependencies.Courses
{
    public static class CoursesDependencies
    {
        public static IServiceCollection ConfigureCoursesServices(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseResponse, CourseResponse>();
            services.AddScoped<ICourseMessageMaker, CourseMessageMaker>();

            return services;
        }
    }
}
