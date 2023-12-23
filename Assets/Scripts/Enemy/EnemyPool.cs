using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : IService
    {
        private const int MaxEnemies = 7;

        private readonly Queue<UnitView> _enemyPool = new();
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

    public class EnemyFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly UnitView _prefab;

        public EnemyFactory(UnitView prefab, AssetProvider assetProvider)
        {
            _prefab = prefab;
            _assetProvider = assetProvider;
        }

        public EnemyController GetEnemy()
        {
            UnitView viewInstance = _assetProvider.Instantiate(_prefab);

            return new EnemyController(viewInstance);
        }
    }
}