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
        // Generates 4 random digits 1-6.
        var random = new Random();
        string output = "";
        while (output.Length < 4)
        {
            output += (random.Next(6) + 1).ToString();
        }
        return output;
    }

    public string GenerateClue(string target, string guess)
    {
        throw new NotImplementedException();
    }
}
