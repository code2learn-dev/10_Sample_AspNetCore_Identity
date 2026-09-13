var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices().ConfigureMiddleware();

app.Run();
