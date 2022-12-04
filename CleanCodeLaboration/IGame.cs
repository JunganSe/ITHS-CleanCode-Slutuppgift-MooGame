namespace CleanCodeLaboration;

public interface IGame
{
    public string GameName { get; init; }
    public string ScoreFileName { get; init; }

    public string GenerateTargetDigits();
    public string GenerateClue(string target, string guess);
}
