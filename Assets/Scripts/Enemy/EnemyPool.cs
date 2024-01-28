using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : IEnemyPool
    {
        private const int MaxEnemies = 7;

        private readonly Queue<EnemyController> _pool = new();
        private Transform _container;
        private Transform _worldTransform;
        private readonly IEnemyFactory _factory;

        public EnemyPool(LevelProvider provider, IEnemyFactory factory)
        {
            _factory = factory;

            LazyInitAsync(provider);
        }

        private async void LazyInitAsync(LevelProvider provider)
        {
            while (provider.Level == null)
                await Task.Yield();
            
            _container = provider.Level.EnemyParent;
            _worldTransform = provider.Level.World;


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