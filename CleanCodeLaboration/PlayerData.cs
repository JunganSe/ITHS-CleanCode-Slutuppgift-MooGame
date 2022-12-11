namespace CleanCodeLaboration;

public class PlayerData
{
    public string Name { get; init; }
    public int GamesCount { get => GuessCounts.Count; }
    public List<int> GuessCounts { get; private set; } = new();

    public PlayerData(string name, int guessCount)
    {
        Name = name;
        AddGameEntry(guessCount);
    }

    public void AddGameEntry(int guessCount)
    {
        GuessCounts.Add(guessCount);
    }

    public double GetAverageGuessCount()
    {
        return GuessCounts.Sum() / (double)GamesCount;
    }
}
