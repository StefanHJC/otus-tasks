
using System;

namespace ShootEmUp
{
    public sealed class PlayerDeathListener : IService, IDisposable
    {
        private HitPointsComponent _playerHealth;

        public event Action PlayerDied;

        public PlayerDeathListener(HitPointsComponent playerHealth)
        {
            _playerHealth = playerHealth;

            _playerHealth.DeathHappened += OnPlayerDeath;
        }

        public void Dispose() => _playerHealth.DeathHappened -= OnPlayerDeath;

        private void OnPlayerDeath() => PlayerDied?.Invoke();
    }
}