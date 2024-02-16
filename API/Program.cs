using API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();
app.UseAuthorization();
app.MapControllers();


// added swagger to see all endpoints easily
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.UseCors("CorsPolicy");


app.Run();
