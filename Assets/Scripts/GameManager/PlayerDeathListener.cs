using System;

namespace ShootEmUp
{
    public sealed class PlayerDeathListener : IService, IDisposable
    {
        private CharacterProvider  _characterProvider;

        public event Action PlayerDied;

        public PlayerDeathListener(CharacterProvider characterProvider)
        {
            _characterProvider = characterProvider;

            _characterProvider.CharacterDied += OnPlayerDeath;
        }

        public void Dispose() => _characterProvider.CharacterDied -= OnPlayerDeath;

        private void OnPlayerDeath() => PlayerDied?.Invoke();
    }
}