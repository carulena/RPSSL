using RpsslGameApi.Domain.Entities;
namespace RpsslGameApi.Contracts;

public class ChoiceResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static ChoiceResponse FromChoice(Choice choice) =>
        new() { Id = (int)choice, Name = choice.ToString().ToLower() };

    public static IEnumerable<ChoiceResponse> All =>
        Enum.GetValues<Choice>().Select(FromChoice);

    public static ChoiceResponse FromId(int id) =>
        FromChoice((Choice)id);

    public static ChoiceResponse FromName(string name)
    {
        if (Enum.TryParse<Choice>(name, ignoreCase: true, out var choice))
            return FromChoice(choice);

        throw new KeyNotFoundException($"Choice '{name}' not found");
    }
}