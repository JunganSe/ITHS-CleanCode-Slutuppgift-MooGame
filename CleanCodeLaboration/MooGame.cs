namespace CleanCodeLaboration;

public class MooGame
{
    private readonly char _correctLetter;
    private readonly char _closeLetter;
    private readonly string _scoreFileName;

    public string PlayerName { get; set; }
    public string Target { get; set; }
    public bool Quit { get; set; }
    public int GuessCount { get; set; }

    public MooGame()
    {
        _correctLetter = 'B';
        _closeLetter = 'C';
        _scoreFileName = "MooGameScores.txt";
        PlayerName = "";
        Target = "";
        Quit = false;
        GuessCount = 0;
    }

    public void Run()
    {
        Initialize();
        while (!Quit)
        {
            MainLoop();
            Console.WriteLine("Correct, it took " + GuessCount + " guesses.\n");
            HandleHighScore();
            Quit = AskToQuit();
        }
    }

    public void Initialize()
    {
        Console.WriteLine("Enter your user name:");
        PlayerName = Console.ReadLine() ?? "Invalid name";
    }

    public void MainLoop()
    {
        Console.WriteLine("New game!\n");
        string target = GenerateTargetDigits();
        Console.WriteLine($"For practice, number is: {target}\n"); // Used for practice.

        string? guess;
        string clue;
        string correctClue = $"{new string(_correctLetter, 4)},";
        do
        {
            guess = Console.ReadLine();
            GuessCount++;
            if (string.IsNullOrEmpty(guess))
                guess = "";
            clue = GenerateClue(target, guess);
            Console.WriteLine($"{clue}\n");
        } while (clue != correctClue);
    }

    public string GenerateTargetDigits()
    {
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

    public void HandleHighScore()
    {
        ScoreHandler.AddEntryToFile(PlayerName, GuessCount, _scoreFileName);
        ShowTopList();
    }

    public void ShowTopList()
    {
        List<PlayerData> playerData = ScoreHandler.GetPlayerDataFromFile(_scoreFileName);
        playerData = playerData.OrderBy(pd => pd.Average).ToList();
        string output = ScoreHandler.StringifyPlayerData(playerData);
        Console.WriteLine(output);
    }

    public bool AskToQuit()
    {
        Console.WriteLine("Play again? (Y/N)");
        string? input = Console.ReadLine();
        if ((!string.IsNullOrEmpty(input)) && (input[..1].ToLower() == "n"))
            return true;
        return false;
    }
}
