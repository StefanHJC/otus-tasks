using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : IService
    {
        private const int MaxEnemies = 7;

        private readonly Queue<EnemyController> _pool = new();
        private readonly AssetProvider _assetProvider;
        private readonly Transform _container;
        private readonly Transform _worldTransform;
        private readonly EnemyFactory _factory;

        public EnemyPool(Transform container, Transform world, EnemyFactory factory, AssetProvider assetProvider)
        {
            _container = container;
            _worldTransform = world;
            _assetProvider = assetProvider;
            _factory = factory;

            Init();
        }

        private void Init()
        {
            for (var i = 0; i < MaxEnemies; i++)
            {
                EnemyController enemy = _factory.GetEnemy();
                enemy.View.transform.parent = _container;
                _pool.Enqueue(enemy);
            }
        }

        public bool TrySpawnEnemy(out EnemyController spawned)
        {
            if (!_pool.TryDequeue(out EnemyController enemy))
            {
                spawned = null;

                return false;
            }

            enemy.View.transform.SetParent(_worldTransform);

            spawned = enemy;
            
            return true;
        }

        public void UnspawnEnemy(EnemyController enemy)
        {
            enemy.View.transform.SetParent(_container);
            _pool.Enqueue(enemy);
        }
    }
}