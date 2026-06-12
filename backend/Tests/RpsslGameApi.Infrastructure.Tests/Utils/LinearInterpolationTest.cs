using NUnit.Framework;
using RpsslGameApi.Infrastructure.Utils;

namespace RpsslGameApi.Infrastructure.Tests.Utils;

[TestFixture]
public class LinearInterpolationTests
{
    [TestCase(1,   1)]
    [TestCase(25,  2)]
    [TestCase(50,  3)]
    [TestCase(75,  4)]
    [TestCase(100, 5)]
    public void Normalize_ReturnsExpectedValue_WhenInputIsValid(int value, int expected)
    {
        var result = LinearInterpolation.Normalize(value);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    public void Normalize_ReturnsValueWithinRange_WhenInputIsValid(int value)
    {
        var result = LinearInterpolation.Normalize(value);

        Assert.That(result, Is.InRange(1, 5));
    }

    [TestCase(0)]
    [TestCase(101)]
    [TestCase(-1)]
    public void Normalize_ThrowsArgumentOutOfRangeException_WhenValueIsOutOfRange(int value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => LinearInterpolation.Normalize(value));
    }

    [Test]
    public void Normalize_ThrowsArgumentException_WhenRawMinIsGreaterThanRawMax()
    {
        Assert.Throws<ArgumentException>(() => LinearInterpolation.Normalize(50, rawMin: 100, rawMax: 1));
    }

    [Test]
    public void Normalize_ThrowsArgumentException_WhenMinIsGreaterThanMax()
    {
        Assert.Throws<ArgumentException>(() => LinearInterpolation.Normalize(50, min: 5, max: 1));
    }

    [Test]
    public void Normalize_ThrowsArgumentException_WhenRawMinEqualsRawMax()
    {
        Assert.Throws<ArgumentException>(() => LinearInterpolation.Normalize(50, rawMin: 50, rawMax: 50));
    }
}