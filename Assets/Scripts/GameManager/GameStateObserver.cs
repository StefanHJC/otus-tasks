namespace ShootEmUp
{
    public class GameStateObserver
    {
        private readonly Game _game;

        public GameState State => _game.State;

        public GameStateObserver(Game game)
        {
            _game = game;
        }
    }
}