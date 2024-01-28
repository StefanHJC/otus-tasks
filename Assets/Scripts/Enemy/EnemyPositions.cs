using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyPositions
    {
        private Transform[] _spawnPositions;
        private Transform[] _attackPositions;

        [Inject]
        public EnemyPositions(LevelProvider provider)
        {
            LazyInitAsync(provider);
        }

        private async void LazyInitAsync(LevelProvider provider)
        {
            while (provider.Level == null)
                await Task.Yield();

            _spawnPositions = provider.Level.EnemySpawnPositions;
            _attackPositions = provider.Level.EnemyAttackPositions;
        }

        public Transform GetRandomSpawnPosition() => GetRandomTransform(_spawnPositions);

        public Transform GetRandomAttackPosition() => GetRandomTransform(_attackPositions);

        private static Transform GetRandomTransform(Transform[] transforms) => transforms[Random.Range(0, transforms.Length)];
    }
}