using RpsslGameApi.Contracts.Options;
using RpsslGameApi.Infrastructure.Mappers;
using RpsslGameApi.Infrastructure.Mappers.Interfaces;
using RpsslGameApi.Infrastructure.Repositories;
using RpsslGameApi.Infrastructure.Repositories.Interface;
using RpsslGameApi.Infrastructure.Services;
using RpsslGameApi.Infrastructure.Services.Interfaces;

namespace RpsslGameApi.Application.StartupExtensions;

public static class  ConfigureDependenciesExtensions
{
    public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpClient<IGetRandomNumberRepository, GetRandomNumberRepository>();
        services.Configure<RandomConfig>(configuration.GetSection("RandomConfig")); 
        services.AddMemoryCache();
        
        #region Services
        services.AddSingleton<IScoreboardService, ScoreboardService>();
        services.AddScoped<IPlayService, PlayService>();
        services.AddScoped<IGetChoiceService, GetChoiceService>();
        services.AddScoped<IGetChoicesService, GetChoicesService>();

        #endregion
        
        #region Mapper

        services.AddSingleton<IPlayMapper, PlayMapper>();
        services.AddSingleton<IGetChoiceMapper, GetChoiceMapper>();
        services.AddSingleton<IGetChoicesMapper, GetChoicesMapper>();

        #endregion
        
        #region Repositories

        services.AddSingleton<IGetRandomNumberRepository, GetRandomNumberRepository>();

        #endregion
    }
}   