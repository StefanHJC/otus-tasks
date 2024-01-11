using System.Threading.Tasks;

namespace ShootEmUp
{
    public sealed class GameLauncher : IService
    {
        private const int GameStartDelay = 3;

        private readonly PlayerInstaller _playerInstaller;
        private readonly GameListenersController _gameListenersController;
        private readonly HUD _hud;

        public GameLauncher(PlayerInstaller playerInstaller, GameListenersController gameListenersController, HUD hud)
        {
            _playerInstaller = playerInstaller;
            _gameListenersController = gameListenersController;
            _hud = hud;
        }

        public async Task StartGameAsync()
        {
            await SetGameStartDelayAsync(delayInSeconds: GameStartDelay);

            _playerInstaller.InstallGameSessionBindings(_playerInstaller.InstantiateCharacterView());
            _hud.PauseButton.Enable();
        }

        private async Task SetGameStartDelayAsync(int delayInSeconds) // Implementation by KISS principle
        {
            int i = 0;
            _hud.ScreenTextRenderer.Enable();

            while (i <= delayInSeconds)
            {
                _hud.ScreenTextRenderer.Text = (delayInSeconds - i++).ToString();
                await Task.Delay(millisecondsDelay: 1000);
            }
            _hud.ScreenTextRenderer.Disable();
        }
    }
}