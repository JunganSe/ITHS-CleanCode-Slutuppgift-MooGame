namespace CleanCodeLaboration;

public class MooGame
{
    private static readonly char _correctLetter = 'B';
    private static readonly char _closeLetter = 'C';
    private static readonly string _scoreFileName = "MooGameScores.txt";

    public static string PlayerName { get; set; } = "";
    public static string Target { get; set; } = "";
    public static bool Quit { get; set; } = false;
    public static int GuessCount { get; set; } = 0;

    public static void Run()
    {
        Initialize();
        while (!Quit)
        {
            MainLoop();
            Console.WriteLine("\nCorrect, it took " + GuessCount + " guesses.\n");
            HandleHighScore();
            Quit = AskToQuit();
        }
    }

    public static void Initialize()
    {
        Console.WriteLine("Enter your user name:");
        PlayerName = Console.ReadLine() ?? "Invalid name";
    }

    public static void MainLoop()
    {
        string target = GenerateTargetDigits();

        Console.WriteLine("New game!");
        Console.WriteLine($"For practice, number is: {target}\n"); // Used for practice.

        string guess = Console.ReadLine() ?? "Invalid guess";

        GuessCount = 1;
        string clue = GenerateClue(target, guess);
        Console.WriteLine(clue + "\n");
        while (clue != "BBBB,")
        {
            GuessCount++;
            guess = Console.ReadLine() ?? "Invalid guess";
            clue = GenerateClue(target, guess);
            Console.WriteLine(clue + "\n");
        }
    }

    public static string GenerateTargetDigits()
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

    public static string GenerateClue(string target, string guess)
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

    public static void HandleHighScore()
    {
        ScoreHandler.AddEntryToFile(PlayerName, GuessCount, _scoreFileName);
        ShowTopList();
    }

    public static void ShowTopList()
    {
        List<PlayerData> playerData = ScoreHandler.GetPlayerDataFromFile(_scoreFileName);
        playerData = playerData.OrderBy(pd => pd.Average).ToList();
        string output = ScoreHandler.StringifyPlayerData(playerData);
        Console.WriteLine(output);
    }

    public static bool AskToQuit()
    {
        Console.WriteLine("Play again? (Y/N)");
        string? input = Console.ReadLine();
        if ((!string.IsNullOrEmpty(input)) && (input[..1].ToLower() == "n"))
            return true;
        return false;
    }
}
