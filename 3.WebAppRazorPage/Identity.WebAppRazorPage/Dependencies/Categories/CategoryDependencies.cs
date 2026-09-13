namespace Identity.WebAppRazorPage.Dependencies.Categories
{
    public static class CategoryDependencies
    {
        public static IServiceCollection ConfigureCategoriesServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryResponse, CategoryResponse>();
            services.AddScoped<ICategoryMessageMaker, CategoryMessageMaker>();

            return services;
        }
    }
}
