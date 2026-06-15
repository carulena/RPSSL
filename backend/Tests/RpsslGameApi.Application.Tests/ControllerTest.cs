using Microsoft.AspNetCore.Mvc;
using Moq;
using RpsslGameApi.Application.Controllers.v1;
using RpsslGameApi.Contracts;
using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Infrastructure.Services.Interfaces;

namespace RpsslGameApi.Application.Tests;

[TestFixture]
public class ControllerTest
{
    private Mock<IGetChoiceService> _choiceServiceMock;
    private Mock<IGetChoicesService> _choicesServiceMock;
    private Mock<IPlayService> _playServiceMock;
    private RpsslGameController _controller;
    private Mock<IScoreboardService>  _scoreboardService;

    [SetUp]
    public void SetUp()
    {
        _choiceServiceMock = new Mock<IGetChoiceService>();
        _choicesServiceMock = new Mock<IGetChoicesService>();
        _playServiceMock = new Mock<IPlayService>();
        _scoreboardService = new Mock<IScoreboardService>();
        _controller = new RpsslGameController(
            _choiceServiceMock.Object,
            _choicesServiceMock.Object,
            _playServiceMock.Object,
            _scoreboardService.Object
        );
    }

    [Test]
    public async Task GetChoices_ReturnsOk_WithAllChoices()
    {
        var choices = ChoiceResponse.All.ToList();
        _choicesServiceMock.Setup(s => s.FetchChoices()).Returns(choices);

        var result = await _controller.GetChoices();

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        Assert.That(ok!.Value, Is.EqualTo(choices));
    }

    [Test]
    public async Task GetChoices_ReturnsOk_WithEmptyList()
    {
        _choicesServiceMock.Setup(s => s.FetchChoices()).Returns([]);

        var result = await _controller.GetChoices();

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var value = ok!.Value as IEnumerable<ChoiceResponse>;
        Assert.That(value, Is.Empty);
    }

    [Test]
    public async Task GetChoice_ReturnsOk_WithChoice()
    {
        var choice = ChoiceResponse.FromChoice(Choice.Rock);
        _choiceServiceMock.Setup(s => s.FetchChoice()).ReturnsAsync(choice);

        var result = await _controller.GetChoice();

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        Assert.That(ok!.Value, Is.EqualTo(choice));
    }

    [Test]
    public async Task GetChoice_ReturnsOk_WithAnyValidChoice()
    {
        var choice = ChoiceResponse.FromChoice(Choice.Spock);
        _choiceServiceMock.Setup(s => s.FetchChoice()).ReturnsAsync(choice);

        var result = await _controller.GetChoice();

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var value = ok!.Value as ChoiceResponse;
        Assert.That(value!.Id, Is.EqualTo(4));
        Assert.That(value.Name, Is.EqualTo("Spock"));
    }

    [Test]
    public async Task Play_ReturnsOk_WithWinResult()
    {
        var response = new PlayResponse { Result = "win", Player = 1, Computer = 3 };
        _playServiceMock.Setup(s => s.PlayGame(1)).ReturnsAsync(response);

        var result = await _controller.Play(new PlayRequest()
        {
            Player = 1
        });

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var value = ok!.Value as PlayResponse;
        Assert.That(value!.Result, Is.EqualTo("win"));
    }

    [Test]
    public async Task Play_ReturnsOk_WithLoseResult()
    {
        var response = new PlayResponse { Result = "lose", Player = 1, Computer = 2 };
        _playServiceMock.Setup(s => s.PlayGame(1)).ReturnsAsync(response);

        var result = await _controller.Play(new PlayRequest()
        {
            Player = 1
        });

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var value = ok!.Value as PlayResponse;
        Assert.That(value!.Result, Is.EqualTo("lose"));
    }

    [Test]
    public async Task Play_ReturnsOk_WithTieResult()
    {
        var response = new PlayResponse { Result = "tie", Player = 1, Computer = 1 };
        _playServiceMock.Setup(s => s.PlayGame(1)).ReturnsAsync(response);

        var result = await _controller.Play(new PlayRequest()
        {
            Player = 1
        });

        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var value = ok!.Value as PlayResponse;
        Assert.That(value!.Result, Is.EqualTo("tie"));
        Assert.That(value.Player, Is.EqualTo(value.Computer));
    }
    
    [Test]
    public async Task Play_ReturnsBadRequest_WhenPlayerIsLessThanOne()
    {
        var result = await _controller.Play(new PlayRequest()
        {
            Player = 0
        });

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
        Assert.That(badRequest!.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task Play_ReturnsBadRequest_WhenPlayerIsGreaterThanFive()
    {
        var result = await _controller.Play(new PlayRequest()
        {
            Player = 6
        });

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
        Assert.That(badRequest!.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task Play_ReturnsBadRequest_WhenPlayerIsZero()
    {
        var result = await _controller.Play(new PlayRequest()
        {
            Player = 0
        });

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.That(badRequest, Is.Not.Null);
    }

    [Test]
    public async Task GetChoice_ReturnsInternalServerError_WhenServiceThrows()
    {
        _choiceServiceMock.Setup(s => s.FetchChoice()).ThrowsAsync(new Exception("Unexpected error"));

        Assert.ThrowsAsync<Exception>(() => _controller.GetChoice());
    }

    [Test]
    public async Task Play_ReturnsNotFound_WhenServiceThrowsKeyNotFoundException()
    {
        _playServiceMock.Setup(s => s.PlayGame(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException("Choice not found"));

        Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.Play(new PlayRequest()
        {
            Player = 1
        }));
    }
}