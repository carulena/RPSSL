namespace RpsslGameApi.Infrastructure.Repositories.Interface;

public interface IGetRandomNumberRepository
{
    Task<int> GetRandomNumberAsync();
}