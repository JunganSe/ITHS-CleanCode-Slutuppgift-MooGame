﻿namespace CleanCodeLaboration;

public class MooGame
{
    private static readonly string _separator = "#&#";
    private static readonly char _correctLetter = 'B';
    private static readonly char _closeLetter = 'C';

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
            HandleHighScore();
            Quit = AskToQuit();
        }
    }

    public static void Initialize()
    {
        Console.WriteLine("Enter your user name:");
        PlayerName = Console.ReadLine() ?? "Invalid name";
        Target = GenerateTargetDigits();

        Console.WriteLine("New game!");
        Console.WriteLine($"For practice, number is: {Target}\n"); // Used for practice.
    }

    public static void MainLoop()
    {
        string guess = Console.ReadLine() ?? "Invalid guess";
        GuessCount = 1;

        string clue = GenerateClue(Target, guess);
        Console.WriteLine(clue + "\n");

        while (clue != "BBBB,")
        {
            GuessCount++;
            guess = Console.ReadLine() ?? "Invalid guess";
            clue = GenerateClue(Target, guess);
            Console.WriteLine(clue + "\n");
        }
    }

    public static void HandleHighScore()
    {
        Console.WriteLine("\nCorrect, it took " + GuessCount + " guesses.\n");

        var streamWriter = new StreamWriter("result.txt", append: true);
        streamWriter.WriteLine(PlayerName + _separator + GuessCount);
        streamWriter.Close();

        ShowTopList();
    }

    public static bool AskToQuit()
    {
        Console.WriteLine("Play again? (Y/N)");
        string? input = Console.ReadLine();
        if ((!string.IsNullOrEmpty(input)) && (input[..1].ToLower() == "n"))
            return true;
        return false;
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

    public static void ShowTopList()
    {
        List<string> fileData = File.ReadAllLines("result.txt")?.ToList() ?? new();
        List<PlayerData> playerData = ParsePlayerData(fileData);
        playerData = playerData.OrderBy(pd => pd.Average).ToList();
        string output = StringifyPlayerData(playerData);
        Console.WriteLine(output);
    }

    public static List<PlayerData> ParsePlayerData(List<string> fileData)
    {
        var entries = new List<KeyValuePair<string, int>>();
        foreach (string line in fileData)
        {
            string[] nameAndGuesses = line.Split(_separator);
            string name = nameAndGuesses[0];
            int guesses = int.Parse(nameAndGuesses[1]);
            entries.Add(new KeyValuePair<string, int>(name, guesses));
        }

        var playerData = new List<PlayerData>();
        foreach (var entry in entries)
        {
            int index = playerData.FindIndex(pd => pd.Name == entry.Key);
            if (index >= 0)
                playerData[index].AddGameEntry(entry.Value);
            else
                playerData.Add(new PlayerData(entry.Key, entry.Value));
        }
        return playerData;
    }

    public static string StringifyPlayerData(List<PlayerData> playerData)
    {
        string format = "{0,-9}{1,5:D}{2,9:F2}";
        string output = string.Format(format, "Player", "Games", "Average");
        foreach (var pd in playerData)
            output += string.Format("\n" + format, pd.Name, pd.NumberOfGames, pd.Average);
        return output;
    }
}
