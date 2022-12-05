namespace CleanCodeLaboration;

public interface IGame
{
    public string Name { get; init; }

    public string GenerateTargetDigits();
    public string GenerateClue(string target, string guess);
}
