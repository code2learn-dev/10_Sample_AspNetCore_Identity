using _10_Identity.WebApiApp.Dependencies.Categories;
using _10_Identity.WebApiApp.Dependencies.Identites;
using _10_Identity.WebApiApp.Dependencies.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
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
			//var policy = new AuthorizationPolicyBuilder()
			//	.RequireAuthenticatedUser()
			//	.Build();
			builder.Services.AddControllers(options =>
			{
				options.Filters.Add(new AuthorizeFilter());
			});
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1",
						new OpenApiInfo()
						{
							Title = "Sample",
							Version = "v1"
						});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter Access Token in the below textbox"
				});

				options.AddSecurityRequirement(
						options => new OpenApiSecurityRequirement
						{
							[new OpenApiSecuritySchemeReference("Bearer")] = []
						});
			});
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
				 bearerOptions.RequireHttpsMetadata = false;
				 bearerOptions.SaveToken = true;
				 bearerOptions.TokenValidationParameters = new TokenValidationParameters()
				 {
					 ValidIssuer = jwtSection["issuer"],
					 ValidAudience = jwtSection["audience"],
					 IssuerSigningKey = securityKey,

					 ValidateIssuer = true,
					 ValidateAudience = true,
					 ValidateLifetime = true,
					 ValidateIssuerSigningKey = true,
					 SaveSigninToken = true,

					 ClockSkew = TimeSpan.Zero
				 };

				 bearerOptions.Events = new JwtBearerEvents()
				 {
					 //OnTokenValidated = e =>
					 //{
					 // Console.WriteLine("================ token received ==============");
					 // return Task.CompletedTask;
					 //},

					 OnTokenValidated = async e =>
					 {
						 var tokenService = e.HttpContext.RequestServices.GetRequiredService<ITokenService>();
						 var tokenResult = await tokenService.ValidateToken(e);
						 if (!tokenResult.Result)
							 e.Fail(tokenResult.Errors.FirstOrDefault() ?? "");
					 },
					 OnForbidden = e =>
					 {
						 Console.WriteLine("================ token forbidden ==============");
						 return Task.CompletedTask;
					 },
					 OnChallenge = e =>
					 {
						 Console.WriteLine("CHALLENGE: " + e.Error);
						 Console.WriteLine("CHALLENGE DESC: " + e.ErrorDescription);
						 return Task.CompletedTask;
					 },
					 OnAuthenticationFailed = e =>
					 {
						 Console.WriteLine("TOKEN FAILED: " + e.Exception?.Message);
						 Console.WriteLine("TOKEN FAILED (inner): " + e.Exception?.InnerException?.Message);
						 return Task.CompletedTask;
					 },
					 OnMessageReceived = e =>
					 {
						 return Task.CompletedTask;
					 }
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
			 
			app.Use(async (ctx, next) =>
			{
				Console.WriteLine($">>> REQUEST {ctx.Request.Method} {ctx.Request.Path}");
				Console.WriteLine($">>> Authorization header: {ctx.Request.Headers["Authorization"].ToString()}");
				await next();
				Console.WriteLine($">>> RESPONSE STATUS: {ctx.Response.StatusCode}");
			});

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

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			return app;
		}
	}
}
