using System.Threading;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public class EnemyAsyncSpawner : IService,
        IGameStartListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameEndListener
    {
        private const int SpawnDelayInMs = 1000;

        private readonly EnemyManager _enemyManager;
        private CancellationTokenSource _cts;
        private bool _isEnabled;

        public EnemyAsyncSpawner(EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
            _isEnabled = true;
        }

        public void OnGameStart() => SpawnEnemiesAsync();

        public void OnGameEnd() => _cts.Cancel();

        public void OnPause() => _isEnabled = false;

        public void OnResume() => _isEnabled = true;

        private async void SpawnEnemiesAsync()
        {
            _cts = new CancellationTokenSource();

            while (_cts.IsCancellationRequested != true)
            {
                await Task.Delay(SpawnDelayInMs, _cts.Token);

                if (!_isEnabled)
                    continue;

                _enemyManager.SpawnEnemy();
            }
        }
    }
}