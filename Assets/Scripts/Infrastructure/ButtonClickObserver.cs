using System;

namespace ShootEmUp
{
    public sealed class ButtonClickObserver : IService, IDisposable
    {
        private readonly HUD _hud;
        private readonly GameListenersController _gameListenersController;
        private readonly GameLauncher _gameLauncher;

        public ButtonClickObserver(HUD hud, GameListenersController gameListenersController, GameLauncher gameLauncher)
        {
            _hud = hud;
            _gameListenersController = gameListenersController;
            _gameLauncher = gameLauncher;

            _hud.StartButton.Clicked += StartGame;
            _hud.PauseButton.Clicked += PauseGame;
            _hud.ResumeButton.Clicked += ResumeGame;
        }

        public void Dispose()
        {
            _hud.StartButton.Clicked -= StartGame;
            _hud.PauseButton.Clicked -= PauseGame;
            _hud.ResumeButton.Clicked -= ResumeGame;
        }

        private void ResumeGame() => _gameListenersController.Resume();

        private void PauseGame() => _gameListenersController.Pause();

        private void StartGame() => _gameLauncher.StartGameAsync();
    }
}