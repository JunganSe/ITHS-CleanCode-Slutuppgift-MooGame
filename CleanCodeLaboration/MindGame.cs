namespace CleanCodeLaboration;

public class MindGame : IGame
{
    private readonly char _correctLetter;
    private readonly char _closeLetter;
    private readonly char _wrongLetter;

    public string Name { get; init; }

    public MindGame()
    {
        _correctLetter = 'B';
        _closeLetter = 'C';
        _wrongLetter = '.';
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
        string output = "";
        guess = guess.PadRight(target.Length);
        for (int i = 0; i < target.Length; i++)
        {
            if (target.Contains(guess[i]))
            {
                output += (guess[i] == target[i])
                    ? _correctLetter
                    : _closeLetter;
            }
            else
                output += _wrongLetter;
        }
        return output;
    }
}
