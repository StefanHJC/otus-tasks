using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : IService
    {
        private const int MaxEnemies = 7;

        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private GameObject _prefab;

        private readonly Queue<GameObject> _enemyPool = new();

        public EnemyPool(Transform container, Transform world, GameObject prefab)
        {
            _container = container;
            _worldTransform = world;
            _prefab = prefab;

            Init();
        }

        private void Init()
        {
            for (var i = 0; i < MaxEnemies; i++)
            {
                GameObject enemy = AssetProvider.Instantiate(_prefab);
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