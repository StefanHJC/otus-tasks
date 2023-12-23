using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : IService
    {
        private const int SpawnDelayInMs = 1000;

        private readonly HashSet<GameObject> _activeEnemies = new();
        private EnemyPositions _enemyPositions;
        private CharacterView _character;
        private EnemyPool _enemyPool;
        private BulletSystem _bulletSystem;
        private CancellationTokenSource _cts;

        public EnemyManager(EnemyPositions enemyPositions, CharacterView character, EnemyPool enemyPool, BulletSystem bulletSystem)
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

                if (_enemyPool.TrySpawnEnemy(spawned: out GameObject enemy))
                {
                    _activeEnemies.Add(enemy);
                    InitEnemy(enemy);
                }
            }
        }

        private void OnDisable() => _cts.Cancel();

        private void InitEnemy(GameObject enemy)
        {
            EnemyAttackAgent attackAgent = enemy.GetComponent<EnemyAttackAgent>();
            
            attackAgent.SetTarget(_character.GetComponent<HitPointsComponent>());
            attackAgent.FirePerformed += OnFire;

            enemy.transform.position = _enemyPositions.GetRandomSpawnPosition().position;
            enemy.GetComponent<HitPointsComponent>().DeathHappened += OnDestroyed;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(_enemyPositions.GetRandomAttackPosition().position);
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().DeathHappened -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().FirePerformed -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(BulletSystem.Args bulletArgs) => _bulletSystem.FlyBulletByArgs(bulletArgs);
    }
}