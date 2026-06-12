using RpsslGameApi.Domain.Entities;
using RpsslGameApi.Infrastructure.Mappers;

namespace RpsslGameApi.Infrastructure.Tests.Mappers;

[TestFixture]
public class GetChoicesMapperTest
{
    private GetChoicesMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _mapper = new GetChoicesMapper();
    }

    [Test]
    public void GetChoices_ReturnsAllFiveChoices()
    {
        var result = _mapper.GetChoices().ToList();

        Assert.That(result, Has.Count.EqualTo(5));
    }

    [Test]
    public void GetChoices_ReturnsCorrectIds()
    {
        var result = _mapper.GetChoices().ToList();
        var expectedIds = Enum.GetValues<Choice>().Select(c => (int)c).ToList();

        Assert.That(result.Select(r => r.Id), Is.EquivalentTo(expectedIds));
    }

    [Test]
    public void GetChoices_ReturnsCorrectNames()
    {
        var result = _mapper.GetChoices().ToList();
        var expectedNames = Enum.GetValues<Choice>().Select(c => c.ToString()).ToList();

        Assert.That(result.Select(r => r.Name), Is.EquivalentTo(expectedNames));
    }

    [Test]
    public void GetChoices_ReturnsNoNullOrEmptyNames()
    {
        var result = _mapper.GetChoices().ToList();

        Assert.That(result.All(r => !string.IsNullOrEmpty(r.Name)), Is.True);
    }

    [Test]
    public void GetChoices_ReturnsUniqueIds()
    {
        var result = _mapper.GetChoices().ToList();

        Assert.That(result.Select(r => r.Id).Distinct().Count(), Is.EqualTo(result.Count));
    }
}