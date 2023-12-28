using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : IService
    {
        private const int SpawnDelayInMs = 1000;

        private readonly HashSet<EnemyController> _activeEnemies = new();
        private EnemyPositions _enemyPositions;
        private CharacterController _character;
        private EnemyPool _enemyPool;
        private BulletSystem _bulletSystem;
        private CancellationTokenSource _cts;

        public EnemyManager(EnemyPositions enemyPositions, CharacterController character, EnemyPool enemyPool, BulletSystem bulletSystem)
        {
            _enemyPositions = enemyPositions;
            _character = character;
            _enemyPool = enemyPool;
            _bulletSystem = bulletSystem;
        }

        private async void Start()
        {
            _cts = new CancellationTokenSource();
            
            while (_cts.IsCancellationRequested != true)
            {
                await Task.Delay(SpawnDelayInMs, _cts.Token);

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
            enemy.Attack(_character.View.transform);
            enemy.FirePerformed += OnFire;

            enemy.View.transform.position = _enemyPositions.GetRandomSpawnPosition().position;
            enemy.View.GetComponent<HitPointsComponent>().DeathHappened += OnDestroyed;
            enemy.View.GetComponent<EnemyMoveAgent>().SetDestination(_enemyPositions.GetRandomAttackPosition().position);
        }

        private void OnDestroyed(EnemyController enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.View.GetComponent<HitPointsComponent>().DeathHappened -= OnDestroyed;
                enemy.View.GetComponent<EnemyAttackAgent>().FirePerformed -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(BulletSystem.Args bulletArgs) => _bulletSystem.FlyBulletByArgs(bulletArgs);
    }
}