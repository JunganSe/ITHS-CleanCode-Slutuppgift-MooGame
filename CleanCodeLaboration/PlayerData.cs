namespace CleanCodeLaboration;

public class PlayerData
{
    private int _totalGuessCount;

    public string Name { get; private set; }
    public int GamesCount { get; private set; }


    public PlayerData(string name, int guessCount)
    {
        Name = name;
        GamesCount = 1;
        _totalGuessCount = guessCount;
    }

    public void AddGameEntry(int guessCount)
    {
        _totalGuessCount += guessCount;
        GamesCount++;
    }

    public double GetAverageGuessCount()
    {
        return (double)_totalGuessCount / GamesCount;
    }
}
