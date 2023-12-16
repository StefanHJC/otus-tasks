using UnityEngine;

namespace ShootEmUp
{
    [DefaultExecutionOrder(-999)]
    public class GameEntry : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private GameObject _enemyPrefab;
        
        [Space]
        [Header("Level settings")]
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _bulletContainer;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private Transform _world;

        [Space]
        [Header("Input settings")]
        [SerializeField] private InputSchema _inputSchema;

        [Space]
        [Header("Player")]
        [SerializeField] private GameObject _character;

        private void Awake()
        {
            ServiceLocator.Bind<BulletBuilder>(new BulletBuilder(_bulletPrefab));
            ServiceLocator.Bind<BulletPool>(new BulletPool(_bulletContainer, ServiceLocator.Get<BulletBuilder>(), _world));
            ServiceLocator.Bind<BulletSystem>(new BulletSystem(_levelBounds, ServiceLocator.Get<BulletPool>()));

            ServiceLocator.Bind<EnemyPool>(new EnemyPool(_enemyContainer, _world, _enemyPrefab));
            ServiceLocator.Bind<EnemyManager>(new EnemyManager(_enemyPositions, _character, ServiceLocator.Get<EnemyPool>(), ServiceLocator.Get<BulletSystem>()));

            ServiceLocator.Bind<InputManager>(new InputManager(_inputSchema));
        }
    }
}