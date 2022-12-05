namespace CleanCodeLaboration;

class Program
{

    public static void Main(string[] args)
    {
        var game = new MooGame();
        var ui = new ConsoleUi();
        var scoreHandler = new ScoreHandler(game.Name);
        var gameController = new GameController(game, ui, scoreHandler);
        gameController.Run();
    }
    
}
