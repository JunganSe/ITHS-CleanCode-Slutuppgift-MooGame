namespace CleanCodeLaboration;

public class ScoreHandler
{
    private readonly string _filePath;

    public string Separator { get; init; }
    public string StringifyFormat { get; init; }

    public ScoreHandler(string gameName)
    {
        _filePath = $"{gameName}Scores.txt";
        StringifyFormat = "{0,-9}{1,5:D}{2,9:F2}";
        Separator = "#&#";
    }

    public void AddEntryToFile(string name, int guessCount)
    {
        string content = name + Separator + guessCount;
        FileHandler.AppendTextFile(_filePath, content);
    }

    public List<PlayerData> GetPlayerDataFromFile()
    {
        var fileData = FileHandler.ReadTextFile(_filePath);
        return ParsePlayerData(fileData);
    }

    public List<PlayerData> ParsePlayerData(List<string> fileData)
    {
        var playerData = new List<PlayerData>();
        foreach (string line in fileData)
        {
            string[] entry = line.Split(Separator);
            string name = entry[0];
            int guessCount = int.Parse(entry[1]);
            int index = playerData.FindIndex(pd => pd.Name == name);
            if (index != -1)
                playerData[index].AddGameEntry(guessCount);
            else
                playerData.Add(new PlayerData(name, guessCount));
        }
        return playerData;
    }

    public string StringifyPlayerData(List<PlayerData> playerData)
    {
        string output = string.Format(StringifyFormat, "Player", "Games", "Average");
        foreach (var pd in playerData)
            output += string.Format($"\n{StringifyFormat}", pd.Name, pd.GamesCount, pd.GetAverageGuessCount());
        return output;
    }
}
