namespace CleanCodeLaboration;

public class GameController
{
    private readonly IGame _game;
    private readonly IUi _ui;
    private readonly ScoreHandler _scoreHandler;

    public string PlayerName { get; private set; } = "Genius2000";
    public string Guess { get; private set; } = "";
    public int GuessCount { get; private set; }
    public string Target { get; private set; } = "";

    public GameController(IGame game, IUi ui, ScoreHandler scoreHandler)
    {
        _game= game;
        _ui= ui;
        _scoreHandler = scoreHandler;
    }

    public void Run()
    {
        AskPlayerName();
        do
        {
            GameLoop();
        } while (AskPlayAgain());
    }

    private void AskPlayerName()
    {
        _ui.PrintOutput("Enter your name:");
        PlayerName = _ui.GetUserInput();
    }

    private void GameLoop()
    {
        InitializeRound();
        do
        {
            MainLoop();
        } while (Guess != Target);
        Congratulate();
        HandleScore();
    }

    private void InitializeRound()
    {
        _ui.PrintOutput($"New game, let's go {PlayerName}!");
        Target = _game.GenerateTargetDigits();
        _ui.PrintOutput($"For practice, number is: {Target}\n"); // Used for practice / testing.
        GuessCount = 0;
    }

    private void MainLoop()
    {
        Guess = _ui.GetUserInput();
        GuessCount++;
        string clue = _game.GenerateClue(Target, Guess);
        _ui.PrintOutput($"{clue}\n");
    }

    private void Congratulate()
    {
        _ui.PrintOutput($"Correct! It took {GuessCount} guesses. Have a medal.");
    }

    private void HandleScore()
    {
        _scoreHandler.AddEntryToFile(PlayerName, GuessCount);
        _ui.PrintOutput(GetScores());
    }

    private string GetScores()
    {
        List<PlayerData> playerData = _scoreHandler.GetPlayerDataFromFile();
        playerData = playerData.OrderBy(pd => pd.GetAverageGuesses()).ToList();
        return _scoreHandler.StringifyPlayerData(playerData);
    }

    private bool AskPlayAgain()
    {
        _ui.PrintOutput("Play again? (Y/N)");
        string input = _ui.GetUserInput();
        return !((!string.IsNullOrEmpty(input)) && (input[..1].ToLower() == "n")); // TODO: Gör mer läsbar.
    }
}
