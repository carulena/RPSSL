using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using RpsslGameApi.Contracts.Options;
using RpsslGameApi.Infrastructure.Repositories;

namespace RpsslGameApi.Infrastructure.Tests.Repository;

[TestFixture]
public class GetRandomNumberRepositoryTests
{
    private GetRandomNumberRepository _repository;
    private Mock<IOptions<RandomConfig>> _randomConfigMock;

    public GetRandomNumberRepositoryTests(GetRandomNumberRepository repository)
    {
        _repository = repository;
    }

    [SetUp]
    public void SetUp()
    {
        _randomConfigMock = new Mock<IOptions<RandomConfig>>();
        _randomConfigMock.Setup(x => x.Value).Returns(new RandomConfig { Url = "https://fake-Url.com/random" });
    }

    private GetRandomNumberRepository CreateRepository(HttpStatusCode statusCode, object? content = null)
    {
        var json = content is null ? "{\"random_number\": 50}" : JsonSerializer.Serialize(content);
        var handler = new FakeHttpMessageHandler(statusCode, json);
        var httpClient = new HttpClient(handler);
        return new GetRandomNumberRepository(_randomConfigMock.Object, httpClient);
    }

    [Test]
    public async Task GetRandomNumberAsync_ReturnsNumber_WhenResponseIsValid()
    {
        _repository = CreateRepository(HttpStatusCode.OK);

        var result = await _repository.GetRandomNumberAsync();

        Assert.That(result, Is.EqualTo(50));
    }

    [Test]
    public async Task GetRandomNumberAsync_ReturnsNumber_WhenRandomNumberIsOne()
    {
        _repository = CreateRepository(HttpStatusCode.OK, new { random_number = 1 });

        var result = await _repository.GetRandomNumberAsync();

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void GetRandomNumberAsync_ThrowsException_WhenResponseIsNotSuccess()
    {
        _repository = CreateRepository(HttpStatusCode.InternalServerError);

        Assert.ThrowsAsync<Exception>(() => _repository.GetRandomNumberAsync());
    }

    [Test]
    public void GetRandomNumberAsync_ThrowsException_WhenResponseIsNotFound()
    {
        _repository = CreateRepository(HttpStatusCode.NotFound);

        Assert.ThrowsAsync<Exception>(() => _repository.GetRandomNumberAsync());
    }

    [Test]
    public void Constructor_ThrowsArgumentException_WhenRandomConfigIsNull()
    {
        var nullConfig = new Mock<IOptions<RandomConfig>>();
        nullConfig.Setup(x => x.Value).Returns((RandomConfig)null!);

        Assert.Throws<ArgumentException>(() =>
        {
            var _ = new GetRandomNumberRepository(nullConfig.Object, new HttpClient());
        }); 
    }
}

public class FakeHttpMessageHandler(HttpStatusCode statusCode, string content) : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        });
    }
}