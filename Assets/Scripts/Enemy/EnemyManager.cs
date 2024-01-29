using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class EnemyManager : IService
    {
        private readonly HashSet<EnemyController> _activeEnemies = new();
        private EnemyPositions _enemyPositions;
        private CharacterProvider _characterProvider;
        private EnemyPool _enemyPool;

        public EnemyManager(EnemyPositions enemyPositions, CharacterProvider characterProvider, EnemyPool enemyPool)
        {
            _enemyPositions = enemyPositions;
            _characterProvider = characterProvider;
            _enemyPool = enemyPool;
        }

        public void SpawnEnemy()
        {
            if (_enemyPool.TrySpawnEnemy(spawned: out EnemyController enemy))
            {
                _activeEnemies.Add(enemy);
                InitEnemy(enemy);
            }
        }

        private void InitEnemy(EnemyController enemy)
        {
            enemy.Attack(_characterProvider.Character.View.transform);

            enemy.View.transform.position = _enemyPositions.GetRandomSpawnPosition().position;
            enemy.Move(to: _enemyPositions.GetRandomAttackPosition().position);
            
            enemy.Died += OnDestroyed;
        }

        private void OnDestroyed(EnemyController enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.Died -= OnDestroyed;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}