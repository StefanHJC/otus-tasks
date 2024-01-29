using System;

namespace ShootEmUp
{
    public sealed class GameEndListener : IService, IDisposable
    {
        private readonly HUD _hud;
        private readonly PlayerDeathListener _playerDeath;

        public GameEndListener(HUD hud, PlayerDeathListener playerDeath)
        {
            _hud = hud;
            _playerDeath = playerDeath;

            _playerDeath.OnGameEnded += OnGameEnd;
        }

        public void Dispose() => _playerDeath.OnGameEnded -= OnGameEnd;

        private void OnGameEnd()
        {
            _hud.ScreenTextRenderer.Enable();
            _hud.ScreenTextRenderer.Text = "Game over!!!";
        }
    }
}