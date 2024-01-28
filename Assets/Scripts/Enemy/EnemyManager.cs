using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : IInitializable, IEnemyManager
    {
        private const int SpawnDelayInMs = 1000;

        private readonly HashSet<EnemyController> _activeEnemies = new();
        private readonly CharacterMoveController _character;
        private readonly EnemyPositions _enemyPositions;
        private readonly IEnemyPool _enemyPool;
        private readonly IBulletSystem _bulletSystem;
        private CancellationTokenSource _cts;
        private bool _isEnabled;

        [Inject]
        public EnemyManager(EnemyPositions enemyPositions, IEnemyPool enemyPool, IBulletSystem bulletSystem)
        {
            _enemyPositions = enemyPositions;
            //_character = character;
            _enemyPool = enemyPool;
            _bulletSystem = bulletSystem;
            _isEnabled = true;
        }

        public void Initialize()
        {
            SpawnEnemiesAsync();
        }

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
                
                if (_enemyPool.TrySpawnEnemy(spawned: out EnemyController enemy))
                {
                    _activeEnemies.Add(enemy);
                    InitEnemy(enemy);
                }
            }
        }

        private void OnDisable() => _cts.Cancel();

        private void InitEnemy(EnemyController enemy)
        {
            //enemy.Attack(_character.View.transform);
            enemy.FirePerformed += OnFire;

            enemy.View.transform.position = _enemyPositions.GetRandomSpawnPosition().position;
            enemy.Move(to: _enemyPositions.GetRandomAttackPosition().position);
            
            enemy.Died += OnDestroyed;
        }

        private void OnDestroyed(EnemyController enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.Died -= OnDestroyed;
                enemy.FirePerformed -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(BulletSystemArgs bulletArgs) => _bulletSystem.FlyBulletByArgs(bulletArgs);
    }
}