namespace CleanCodeLaboration;

public class MooGame : IGame
{
    private readonly char _correctLetter;
    private readonly char _closeLetter;

    public string Name { get; init; }
    public string Instructions { get; init; }

    public MooGame()
    {
        _correctLetter = 'B';
        _closeLetter = 'C';
        Name = "MooGame";
        Instructions = $"\nGuess the 4 digits!\nA clue will show you the way:\nEvery {_correctLetter} means a digit is in the correct place.\nEvery {_closeLetter} means a digit is used but in another place.\n";
    }

    public string GenerateTargetDigits()
    {
        // Generates 4 unique digits.
        var random = new Random();
        string output = "";
        while (output.Length < 4)
        {
            string newDigit = random.Next(10).ToString();
            if (!output.Contains(newDigit))
                output += newDigit;
        }
        return output;
    }

    public string GenerateClue(string target, string guess)
    {
        string correct = "";
        string close = "";
        guess = guess.PadRight(target.Length);

        for (int i = 0; i < target.Length; i++)
        {
            if (target.Contains(guess[i]))
            {
                if (guess[i] == target[i])
                    correct += _correctLetter;
                else
                    close += _closeLetter;
            }
        }
        return $"{correct},{close}";
    }
}
