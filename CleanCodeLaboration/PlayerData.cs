namespace CleanCodeLaboration;

public class PlayerData
{
    private int _totalGuessCount;

    public string Name { get; private set; }
    public int GamesCount { get; private set; }


    public PlayerData(string name, int guesses)
    {
        Name = name;
        GamesCount = 1;
        _totalGuessCount = guesses;
    }

    public void AddGameEntry(int guesses)
    {
        _totalGuessCount += guesses;
        GamesCount++;
    }

    public double GetAverageGuesses()
    {
        return (double)_totalGuessCount / GamesCount;
    }
}
