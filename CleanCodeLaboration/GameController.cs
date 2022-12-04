namespace CleanCodeLaboration;

public class GameController
{
    private IGame _game;
    private IUi _ui;
    public GameController(IGame game, IUi ui)
    {
        _game= game;
        _ui= ui;
    }

    public void RunGame()
    {

    }
}
