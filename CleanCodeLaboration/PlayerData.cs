namespace CleanCodeLaboration;

public class PlayerData
{
    private int _totalGuesses;

    public string Name { get; private set; }
    public int NumberOfGames { get; private set; }


    public PlayerData(string name, int guesses)
    {
        Name = name;
        NumberOfGames = 1;
        _totalGuesses = guesses;
    }

    public void AddGameEntry(int guesses)
    {
        _totalGuesses += guesses;
        NumberOfGames++;
    }

    public double GetAverageGuesses()
    {
        return (double)_totalGuesses / NumberOfGames;
    }
}
