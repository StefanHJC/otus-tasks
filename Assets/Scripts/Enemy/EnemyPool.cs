using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : IService
    {
        private const int MaxEnemies = 7;

        private readonly Queue<GameObject> _enemyPool = new();
        private readonly AssetProvider _assetProvider;
        private readonly Transform _container;
        private readonly Transform _worldTransform;
        private readonly GameObject _prefab;

        public EnemyPool(Transform container, Transform world, GameObject prefab, AssetProvider assetProvider)
        {
            _container = container;
            _worldTransform = world;
            _prefab = prefab;
            _assetProvider = assetProvider;

            Init();
        }

        private void Init()
        {
            for (var i = 0; i < MaxEnemies; i++)
            {
                GameObject enemy = _assetProvider.Instantiate(_prefab);
                enemy.transform.parent = _container;
                _enemyPool.Enqueue(enemy);
            }
        }

        public bool TrySpawnEnemy(out GameObject spawned)
        {
            if (!_enemyPool.TryDequeue(out GameObject enemy))
            {
                spawned = null;

                return false;
            }

            enemy.transform.SetParent(_worldTransform);

            spawned = enemy;
            
            return true;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }
    }
}