namespace CleanCodeLaboration;

public class GameController
{
    private readonly IGame _game;
    private readonly IUi _ui;
    private readonly ScoreHandler _scoreHandler;

    public string PlayerName { get; private set; } = "Genius2000";
    public string Guess { get; private set; } = "";
    public int GuessCount { get; private set; } = 0;
    public string Target { get; private set; } = "";

    public GameController(IGame game, IUi ui, ScoreHandler scoreHandler)
    {
        _game = game;
        _ui = ui;
        _scoreHandler = scoreHandler;
    }

    public void Run()
    {
        PlayerName = AskPlayerName();
        _ui.PrintOutput(_game.Instructions);
        do
        {
            GameLoop();
        } while (Ask("Play again?"));
    }

    public string AskPlayerName()
    {
        _ui.PrintOutput("Enter your name:");
        return _ui.GetUserInput();
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
        Target = _game.GenerateTargetDigits();
        if (Ask("Cheat?"))
            _ui.PrintOutput($"For practice, number is: {Target}\n");
        _ui.PrintOutput($"New game, let's go {PlayerName}!\n");
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
        _ui.PrintOutput($"Correct! It took {GuessCount} guesses. Have a medal.\n");
    }

    private void HandleScore()
    {
        var entry = new List<string> { PlayerName, GuessCount.ToString() };
        _scoreHandler.AddEntryToFile(entry);
        _ui.PrintOutput(GetScores() + "\n");
    }

    private string GetScores()
    {
        List<PlayerData> playerData = _scoreHandler.GetPlayerDataFromFile();
        playerData = playerData.OrderBy(pd => pd.GetAverageGuessCount()).ToList();
        return _scoreHandler.StringifyPlayerData(playerData);
    }

    public bool Ask(string question)
    {
        _ui.PrintOutput($"{question} (Y/N)");
        string input = _ui.GetUserInput();
        return !((!string.IsNullOrEmpty(input)) && (input[..1].ToLower() == "n")); // TODO: Gör mer läsbar.
    }
}
