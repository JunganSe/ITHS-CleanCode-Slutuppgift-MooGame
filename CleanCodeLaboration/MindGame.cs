namespace CleanCodeLaboration;

public class MindGame : IGame
{
    private readonly char _correctLetter;
    private readonly char _closeLetter;

    public string Name { get; init; }

    public MindGame()
    {
        _correctLetter = 'B';
        _closeLetter = 'C';
        Name = "MindGame";
    }

    public string GenerateTargetDigits()
    {
        throw new NotImplementedException();
    }
    public string GenerateClue(string target, string guess)
    {
        throw new NotImplementedException();
    }
}
