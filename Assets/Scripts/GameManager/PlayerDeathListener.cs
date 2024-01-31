
using System;
using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerDeathListener : IDisposable, IGameEndListener
    {
        private HitPointsComponent _playerHealth;

        public event Action OnGameEnded;

        [Inject]
        public PlayerDeathListener(CharacterProvider provider)
        {
            LazyInitAsync(provider);
        }

        public void Dispose() => _playerHealth.OnDeath -= OnPlayerOnDeath;

        private async void LazyInitAsync(CharacterProvider provider)
        {
            while (provider.Character == null)
                await Task.Yield();

            _playerHealth = provider.CharacterHealth;
            _playerHealth.OnDeath += OnPlayerOnDeath;
        }

        private void OnPlayerOnDeath() => OnGameEnded?.Invoke();
    }
}