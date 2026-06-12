
using RpsslGameApi.Contracts;
using RpsslGameApi.Infrastructure.Mappers;

namespace RpsslGameApi.Infrastructure.Tests.Mappers;

[TestFixture]
public class GetChoiceMapperTest
{
    private GetChoiceMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _mapper = new GetChoiceMapper();
    }

    [TestCase(1)]
    [TestCase(20)]
    [TestCase(50)]
    [TestCase(80)]
    [TestCase(100)]
    public void GetChoice_ReturnsChoiceResponse_WhenRandomNumberIsValid(int randomNumber)
    {

        var result = _mapper.GetChoice(randomNumber);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.InRange(1, 5));
        Assert.That(result.Name, Is.Not.Empty);
    }

    [Test]
    public void GetChoice_ThrowsArgumentOutOfRangeException_WhenRandomNumberIsZero()
    {
        var input = 0;

        Assert.Throws<ArgumentOutOfRangeException>(() => _mapper.GetChoice(input));
    }

    [Test]
    public void GetChoice_ThrowsArgumentOutOfRangeException_WhenRandomNumberIsGreaterThan100()
    {
        var input = 101;

        Assert.Throws<ArgumentOutOfRangeException>(() => _mapper.GetChoice(input));
    }

    [Test]
    public void GetChoice_ThrowsArgumentOutOfRangeException_WhenRandomNumberIsNegative()
    {
        var input = -1;

        Assert.Throws<ArgumentOutOfRangeException>(() => _mapper.GetChoice(input));
    }
}