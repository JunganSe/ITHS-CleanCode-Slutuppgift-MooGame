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
        string output = string.Format(_stringifyFormat, "Player", "Games", "Average");
        foreach (var pd in playerData)
            output += string.Format($"\n{_stringifyFormat}", pd.Name, pd.GamesCount, pd.GetAverageGuessCount());
        return output;
    }
}
