namespace ShootEmUp
{
    public class GameStateObserver : IService
    {
        private readonly Game _game;

        public GameState State => _game.State;

        public GameStateObserver(Game game)
        {
            _game = game;
        }
    }
}