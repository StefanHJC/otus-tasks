using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public Transform GetRandomSpawnPosition() => GetRandomTransform(_spawnPositions);

        public Transform GetRandomAttackPosition() => GetRandomTransform(_attackPositions);

        private static Transform GetRandomTransform(Transform[] transforms) => transforms[Random.Range(0, transforms.Length)];
    }
}