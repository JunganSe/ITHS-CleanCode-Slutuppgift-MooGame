namespace CleanCodeLaboration;

public class GameController
{
    private readonly IGame _game;
    private readonly IUi _ui;

    public string PlayerName { get; set; } = "Genius2000";

    public GameController(IGame game, IUi ui)
    {
        _game= game;
        _ui= ui;
    }

    public void Run()
    {
        AskPlayerName();
        do
        {
            _ui.PrintOutput("New game, let's go!");
            string target = _game.GenerateTargetDigits();
            Console.WriteLine($"For practice, number is: {target}\n"); // Used for practice / testing.
            string guess;
            int guessCount = 0;
            do
            {
                guess = _ui.GetUserInput();
                guessCount++;
                string clue = _game.GenerateClue(target, guess);
                _ui.PrintOutput(clue);
            } while (guess != target);
            _ui.PrintOutput($"Correct! It took {guessCount} guesses.");
            ScoreHandler.AddEntryToFile(PlayerName, guessCount, _game.ScoreFileName);
            ShowTopList();
        } while (AskPlayAgain());
    }

    private void AskPlayerName()
    {
        _ui.PrintOutput("Enter your name:");
        PlayerName = _ui.GetUserInput();
    }

    private bool AskPlayAgain()
    {
        _ui.PrintOutput("Play again? (Y/N)");
        string input = _ui.GetUserInput();
        return !((!string.IsNullOrEmpty(input)) && (input[..1].ToLower() == "n")); // TODO: Gör mer läsbar.
    }

    private void ShowTopList()
    {
        List<PlayerData> playerData = ScoreHandler.GetPlayerDataFromFile(_game.ScoreFileName);
        playerData = playerData.OrderBy(pd => pd.Average).ToList();
        string output = ScoreHandler.StringifyPlayerData(playerData);
        _ui.PrintOutput(output);
    }
}
