using RpsslGameApi.Application.Extensions;
using RpsslGameApi.Application.Filters;
using RpsslGameApi.Application.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
            {
                var uri = new Uri(origin);
                return uri.Host == "localhost" || uri.Host == "127.0.0.1";
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();
builder.Services.ConfigureDependencies(builder.Configuration, builder.Environment);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseSwaggerConfig();

app.MapControllers();
app.Run();