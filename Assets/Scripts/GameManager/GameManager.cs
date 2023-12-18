using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : IService
    {
        private readonly GameListenersController _gameListenersController;

        public GameManager(GameListenersController gameListenersController)
        {
            _gameListenersController = gameListenersController;
        }

        public void Pause() => _gameListenersController.Pause();

        public void Resume() => _gameListenersController.Resume();

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}