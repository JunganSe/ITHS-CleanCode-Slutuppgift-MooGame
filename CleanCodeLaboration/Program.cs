namespace CleanCodeLaboration;

class Program
{

    public static void Main(string[] args)
    {
        var game = new MooGame();
        var ui = new ConsoleUi();
        var gameController = new GameController(game, ui);
        gameController.Run();
    }
    
}
