namespace CleanCodeLaboration;

public class ScoreHandler
{
    private readonly string _filePath;
    private readonly string _stringifyFormat;

    public string Separator { get; init; }

    public ScoreHandler(string gameName)
    {
        _filePath = $"{gameName}Scores.txt";
        _stringifyFormat = "{0,-9}{1,5:D}{2,9:F2}";
        Separator = "#&#";
    }

    public List<string> ReadTextFile(string path)
    {
        return File.ReadAllLines(path)?.ToList() ?? new();
    }

    public void AddEntryToFile(string name, int guessCount)
    {
        var streamWriter = new StreamWriter(_filePath, append: true);
        streamWriter.WriteLine(name + Separator + guessCount);
        streamWriter.Close();
    }

    public List<PlayerData> GetPlayerDataFromFile()
    {
        List<string> fileData = ReadTextFile(_filePath);
        return ParsePlayerData(fileData);
    }

    public List<PlayerData> ParsePlayerData(List<string> fileData)
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

    public string StringifyPlayerData(List<PlayerData> playerData)
    {
        string output = string.Format(_stringifyFormat, "Player", "Games", "Average");
        foreach (var pd in playerData)
            output += string.Format($"{_stringifyFormat}", pd.Name, pd.GamesCount, pd.GetAverageGuesses());
        return output;
    }
}
