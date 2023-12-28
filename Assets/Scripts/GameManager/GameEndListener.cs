using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEndListener : IService, IStartListener
    {
        private HitPointsComponent _playerHealth;
        private GameManager _gameManager;

        public GameEndListener(HitPointsComponent playerHealth, GameManager gameManager)
        {
            _playerHealth = playerHealth;
            _gameManager = gameManager;
        }

        public void OnStart()
        {
            _playerHealth.DeathHappened += OnPlayerDeath;
        }

        private void OnPlayerDeath() => _gameManager.FinishGame();
    }
}