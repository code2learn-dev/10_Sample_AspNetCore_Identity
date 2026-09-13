namespace _10_Identity.WebApiApp.Dependencies
{
    public static class WebApiApplicationServices
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // add EF core service with Sql Server provider
            builder.Services.AddDbContext<AcademyDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(local);Initial Catalog=sample_aspnetcore_identity_academy;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=True;");
            });

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();

            return builder;
        }

        public static WebApplication ConfigureMiddleware(this WebApplicationBuilder builder)
        {
            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();

                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    config.RoutePrefix = string.Empty;
                });
            }

            app.MapControllers();

            return app;
        }
    }
}
