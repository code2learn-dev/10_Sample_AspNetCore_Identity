namespace _10_Identity.WebApiApp.Dependencies
{
    public static class CommonDependencies
    {
        public static IServiceCollection RegisterPublicServices(this IServiceCollection services)
        {
            // register webapplication services layer mapping services
            services.AddAutoMapper(a => a.AddProfile(new DtoMappingProfile()));
            // register web api mapping services
            services.AddAutoMapper(a => a.AddProfile(new MappingProfile()));

            // add model validator service that has injected in all services
            services.AddScoped<IModelValidator, ModelValidator>();
            
            return services;
        }
    }
}
