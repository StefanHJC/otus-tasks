using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private const int SpawnDelay = 1;

        [Header("Spawn")]
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private GameObject _character;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> _activeEnemies = new();

        private void Start() => StartCoroutine(SpawnEnemyRoutine());

        private IEnumerator SpawnEnemyRoutine()
        {
            while (gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(SpawnDelay);

                if (_enemyPool.TrySpawnEnemy(spawned: out GameObject enemy))
                {
                    _activeEnemies.Add(enemy);
                    InitEnemy(enemy);
                }
            }
        }

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