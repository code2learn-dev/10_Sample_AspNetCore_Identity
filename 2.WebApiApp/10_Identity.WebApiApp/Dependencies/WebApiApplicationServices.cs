using _10_Identity.WebApiApp.Dependencies.Categories;
using _10_Identity.WebApiApp.Dependencies.Identites;
using _10_Identity.WebApiApp.Dependencies.Tokens;
using Identity.ApplicationService.Tokens.Entities;
using Identity.ApplicationService.Tokens.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

			// add web api service 
			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();
			builder.Services.AddEndpointsApiExplorer();

			// add automapper service
			builder.Services.AddAutoMapper(a => a.AddProfile(new DtoMappingProfile()));


			builder.Services.Configure<JwtSectionConfiguration>(
					builder.Configuration.GetSection("jwt"));

			// add jwt bearer authenticatin configuration
			var jwtSection = builder.Configuration.GetSection("jwt");
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["key"]));
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			 .AddJwtBearer(bearerOptions =>
			 {
				 bearerOptions.TokenValidationParameters = new TokenValidationParameters()
				 {
					 ValidIssuer = jwtSection["issuer"],
					 ValidAudience = jwtSection["audience"],
					 IssuerSigningKey = securityKey,
					 ValidateLifetime = true,
					 ValidateIssuerSigningKey = true,
					 SaveSigninToken = true
				 };

				 bearerOptions.Events = new JwtBearerEvents()
				 {
					 OnTokenValidated = async e =>
					 {
						 var tokenService = e.HttpContext.RequestServices.GetRequiredService<ITokenService>(); 
						 var tokenResult = await tokenService.ValidateToken(e);
						 if (!tokenResult.Result) 
                             e.Fail(tokenResult.Errors.FirstOrDefault() ?? ""); 
                     },
					 OnForbidden = e => { return Task.CompletedTask; },
					 OnChallenge = e => { return Task.CompletedTask; },
					 OnAuthenticationFailed = e => { return Task.CompletedTask; },
					 OnMessageReceived = e => { return Task.CompletedTask; }
				 };
			 });

			// register application services
			builder.Services
				// add common services
				.RegisterPublicServices()
				// configure token services
				.ConfigureTokenServices()
				// add categories services
				.RegisterCategoriesService();


			// configure identity services
			builder.Services.ConfigureIdentityServices()
							.ConfigureIdentityOptions()
							.ConfigureIdentityTokenProvider();

			return builder;
		}

		public static WebApplication ConfigureMiddleware(this WebApplicationBuilder builder)
		{
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
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
