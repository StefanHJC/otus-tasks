using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private const int SpawnDelay = 1;

        [Header("Spawn")]
        [SerializeField]
        private EnemyPositions enemyPositions;

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private void Start() => StartCoroutine(SpawnEnemyRoutine());

        private IEnumerator SpawnEnemyRoutine()
        {
            while (gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(SpawnDelay);

                var enemy = this._enemyPool.SpawnEnemy();

                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        InitEnemy(enemy);
                    }
                }
            }
        }

        private void InitEnemy(GameObject enemy)
        {
            enemy.transform.position = this.enemyPositions.RandomSpawnPosition().position;
            enemy.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(enemyPositions.RandomAttackPosition().position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this.character.GetComponent<HitPointsComponent>());
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(BulletSystem.Args bulletArgs) => _bulletSystem.FlyBulletByArgs(bulletArgs);
    }
}