using Zenject;

namespace ShootEmUp
{
    public sealed class ZenjectTickableInterruptionController : IGameInterruptionController
    {
        private readonly TickableManager _tickableManager;
        private readonly ButtonClickListener _buttonListener;
        private readonly Game _game;
        private readonly IBulletSystem _bulletSystem;

        [Inject]
        public ZenjectTickableInterruptionController(TickableManager tickableManager, IBulletSystem bulletSystem, 
                                                    ButtonClickListener buttonListener, Game game)
        {
            _tickableManager = tickableManager;
            _bulletSystem = bulletSystem;
            _buttonListener = buttonListener;
            _game = game;

            _buttonListener.OnPauseFired += Pause;
            _buttonListener.OnResumeFired += Resume;
        }

        public void Pause()
        {
            if (_game.State != GameState.Playing)
                return;

            _game.State = GameState.Paused;
            _tickableManager.IsPaused = true;
            _bulletSystem.OnPause();
        }

        public void Resume()
        {
            if ( _game.State != GameState.Paused)
                return;

            _game.State = GameState.Playing;
            _tickableManager.IsPaused = false;
            _bulletSystem.OnResume();
        }
    }
}