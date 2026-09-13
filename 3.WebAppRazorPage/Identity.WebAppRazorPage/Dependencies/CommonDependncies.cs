namespace Identity.WebAppRazorPage.Dependencies
{
    public static class CommonDependncies
    {
        public static void InjectCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IModelValidator, ModelValidator>();
        }
    }
}
