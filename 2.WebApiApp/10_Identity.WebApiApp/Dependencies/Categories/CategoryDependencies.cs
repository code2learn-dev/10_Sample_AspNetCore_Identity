namespace _10_Identity.WebApiApp.Dependencies.Categories
{
    public static class CategoryDependencies
    {
        public static IServiceCollection RegisterCategoriesService(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryMessageMaker, CategoryMessageMaker>();
            services.AddScoped<ICategoryResponse, CategoryResponse>();
            return services;
        }
    }
}
