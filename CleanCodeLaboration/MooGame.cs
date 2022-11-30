namespace CleanCodeLaboration;

public class MooGame
{
    private static readonly string _separator = "#&#";

    public void Run()
    {
        bool playOn = true;
        Console.WriteLine("Enter your user name:\n");
        string name = Console.ReadLine();

        while (playOn)
        {
            string goal = GenerateTargetDigits();

            Console.WriteLine("New game:\n");
            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + goal + "\n");
            string guess = Console.ReadLine();

            int nGuess = 1;
            string bbcc = GenerateClue(goal, guess);
            Console.WriteLine(bbcc + "\n");
            while (bbcc != "BBBB,")
            {
                nGuess++;
                guess = Console.ReadLine();
                Console.WriteLine(guess + "\n");
                bbcc = GenerateClue(goal, guess);
                Console.WriteLine(bbcc + "\n");
            }
            StreamWriter output = new StreamWriter("result.txt", append: true);
            output.WriteLine(name + "#&#" + nGuess);
            output.Close();
            ShowTopList();
            Console.WriteLine("Correct, it took " + nGuess + " guesses\nContinue?");
            string answer = Console.ReadLine();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                playOn = false;
            }
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
                    correct+= "B";
                else
                    close += "C";
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
        string output = string.Format("{0,-9}{1,5:D}{2,9:F2}", "Player", "Games", "Average");
        foreach (var pd in playerData)
            output += string.Format("\n{0,-9}{1,5:D}{2,9:F2}", pd.Name, pd.NumberOfGames, pd.Average);
        return output;
    }
}
