using UnityEngine;

namespace ShootEmUp
{
    public class Level : MonoBehaviour
    {
        [Header("Enemy positions")]
        [SerializeField] private Transform[] _enemySpawnPositions;
        [SerializeField] private Transform[] _enemyAttackPositions;
        [SerializeField] private Transform _enemyParent;
        
        [Space]
        [Header("Bullet")]
        [SerializeField] private Transform _bulletParent;

        [Space]
        [Header("Level background")]
        [SerializeField] private Transform _levelBackground;

        [Space]
        [Header("Level bounds")]
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _downBorder;
        [SerializeField] private Transform _topBorder;

        [Space]
        [Header("World")]
        [SerializeField] private Transform _world;


        public Transform[] EnemySpawnPositions => _enemySpawnPositions;
        public Transform[] EnemyAttackPositions => _enemyAttackPositions;
        public Transform World => _world;
        public Transform BulletParent => _bulletParent;
        public Transform EnemyParent => _enemyParent;
        public Transform LeftBorder => _rightBorder;
        public Transform RightBorder => _leftBorder;
        public Transform DownBorder => _downBorder;
        public Transform TopBorder => _topBorder;
    }
}