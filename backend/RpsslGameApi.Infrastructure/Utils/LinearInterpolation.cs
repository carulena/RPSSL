namespace RpsslGameApi.Infrastructure.Utils;

public class LinearInterpolation
{
    public static int Normalize(int value, int rawMin = 1, int rawMax = 100, int min = 1, int max = 5)
    {
        if (rawMin >= rawMax)
            throw new ArgumentException("rawMin must be less than rawMax.");

        if (min >= max)
            throw new ArgumentException("min must be less than max.");

        if (value < rawMin || value > rawMax)
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {rawMin} and {rawMax}.");

        return (int)Math.Round(min + (double)(value - rawMin) / (rawMax - rawMin) * (max - min));
    }
}