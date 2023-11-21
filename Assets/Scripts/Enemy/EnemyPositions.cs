using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public Transform RandomSpawnPosition() => RandomTransform(_spawnPositions);

        public Transform RandomAttackPosition() => RandomTransform(_attackPositions);

        private static Transform RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length);
            
            return transforms[index];
        }
    }
}