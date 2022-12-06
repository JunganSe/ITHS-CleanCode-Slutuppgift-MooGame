namespace CleanCodeLaboration;

public class MindGame : IGame
{
    private readonly char _correctLetter;
    private readonly char _closeLetter;
    private readonly char _wrongLetter;

    public string Name { get; init; }
    public string Instructions { get; init; }

    public MindGame()
    {
        _correctLetter = 'B';
        _closeLetter = 'C';
        _wrongLetter = '.';
        Name = "MindGame";
        Instructions = $"\nGuess the 4 digits!\nA clue will show for each digit you guessed:\n{_correctLetter}: Correct\n{_closeLetter}: Correct but in wrong place\n{_wrongLetter}: Wrong digit\n";
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
