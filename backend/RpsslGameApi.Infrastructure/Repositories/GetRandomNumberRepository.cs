using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using RpsslGameApi.Contracts;
using RpsslGameApi.Contracts.Options;
using RpsslGameApi.Infrastructure.Repositories.Interface;
namespace RpsslGameApi.Infrastructure.Repositories;

public class GetRandomNumberRepository:IGetRandomNumberRepository
{
    private readonly RandomConfig _randomConfig;
    private readonly HttpClient _httpClient;

    public GetRandomNumberRepository(
        IOptions<RandomConfig> randomConfig,
        HttpClient httpClient
    )
    {
        _randomConfig = randomConfig.Value ?? throw new ArgumentException(nameof(randomConfig));
        _httpClient = httpClient;
    }

    public async Task<int> GetRandomNumberAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_randomConfig.Url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RandomNumberResponse>();
        
            if (result is null)
                throw new NullReferenceException("Response body is null.");

            if (result.RandomNumber < 1 || result.RandomNumber > 100)
                throw new ArgumentOutOfRangeException(nameof(result.RandomNumber), "Random number must be between 1 and 100.");

            return result.RandomNumber;
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Failed to fetch random number: {e.Message}");
        }
        catch (TaskCanceledException)
        {
            throw new TimeoutException("Request timed out while fetching random number.");
        }
    }
}