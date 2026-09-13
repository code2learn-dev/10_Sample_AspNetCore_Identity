using Identity.Domain.IDentityContent;
using Microsoft.EntityFrameworkCore;
using Identity.WebAppRazorPage.Dependencies; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AcademyDbContext>(options =>
{
	options.UseSqlServer("Data Source=(local);Initial Catalog=sample_aspnetcore_identity_academy;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=True;");
});

// configure services and pipeline
var app = builder.ConfigureServices().ConfigurePipeline();

// running application
app.Run();
