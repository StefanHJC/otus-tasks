using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private const int SpawnDelayInMs = 1000;

        [Header("Spawn")]
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private GameObject _character;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> _activeEnemies = new();
        private CancellationTokenSource _cts;

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

            enemy.transform.position = _enemyPositions.RandomSpawnPosition().position;
            enemy.GetComponent<HitPointsComponent>().DeathHappened += OnDestroyed;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(_enemyPositions.RandomAttackPosition().position);
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