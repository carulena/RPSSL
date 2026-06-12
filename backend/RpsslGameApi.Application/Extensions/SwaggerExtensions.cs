using Asp.Versioning;
using Microsoft.OpenApi.Models;
namespace RpsslGameApi.Application.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Rock Paper Scissors Spock Lizard Game API",
                Version = "v1"
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerConfig(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
        app.MapGet("/", () => Results.Redirect("/swagger"));

        return app;
    }
}