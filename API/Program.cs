using API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
