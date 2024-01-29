using System;

namespace ShootEmUp
{
    public sealed class PlayerDeathListener : IService, IDisposable
    {
        private readonly GameListenersController _gameListenersController;
        private readonly CharacterProvider  _characterProvider;

        public event Action OnGameEnded;

        public PlayerDeathListener(CharacterProvider characterProvider, GameListenersController gameListenersController)
        {
            _characterProvider = characterProvider;
            _gameListenersController = gameListenersController;

            _characterProvider.CharacterDied += OnPlayerDeath;
        }

        public void Dispose() => _characterProvider.CharacterDied -= OnPlayerDeath;

        private void OnPlayerDeath()
        {
            _gameListenersController.EndGame();
            
            OnGameEnded?.Invoke();
        }
    }
}