namespace CleanCodeLaboration;

public class ScoreHandler
{
    public static string Separator { get; } = "#&#";

    public static List<PlayerData> GetPlayerDataFromFile(string path)
    {
        List<string> fileData = ReadTextFile(path);
        return ParsePlayerData(fileData);
    }

    public static List<string> ReadTextFile(string path)
    {
        return File.ReadAllLines(path)?.ToList() ?? new();
    }

    public static List<PlayerData> ParsePlayerData(List<string> fileData)
    {
        var entries = new List<KeyValuePair<string, int>>();
        foreach (string line in fileData)
        {
            string[] nameAndGuesses = line.Split(Separator);
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
