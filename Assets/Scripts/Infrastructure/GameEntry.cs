using UnityEngine;

namespace ShootEmUp
{
    [DefaultExecutionOrder(-9999)]
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
        [SerializeField] private Transform _characterStartPosition;

        [Space]
        [Header("Input settings")]
        [SerializeField] private InputSchema _inputSchema;

        [Space]
        [Header("Player")]
        [SerializeField] private CharacterView _characterView;

        [Space]
        [Header("Game listeners controller")]
        [SerializeField] private GameListenersController _listenersController;

        private void Awake()
        {
            BindServices();
            StartGame();
        }

        private CharacterView StartGame() => ServiceLocator.Get<GameManager>().StartGame(_characterStartPosition, _characterView);

        private void BindServices()
        {
            ServiceLocator.Init(_listenersController);

            ServiceLocator.Bind<AssetProvider>(new AssetProvider());
            ServiceLocator.Bind<GameManager>(new GameManager(_listenersController, ServiceLocator.Get<AssetProvider>()));

            ServiceLocator.Bind<BulletBuilder>(new BulletBuilder(_bulletPrefab, ServiceLocator.Get<AssetProvider>()));
            ServiceLocator.Bind<BulletPool>(new BulletPool(_bulletContainer, ServiceLocator.Get<BulletBuilder>(), _world));
            ServiceLocator.Bind<BulletSystem>(new BulletSystem(_levelBounds, ServiceLocator.Get<BulletPool>()));

            ServiceLocator.Bind<EnemyPool>(new EnemyPool(_enemyContainer, _world, _enemyPrefab,
                ServiceLocator.Get<AssetProvider>()));
            ServiceLocator.Bind<EnemyManager>(new EnemyManager(_enemyPositions, _characterView, ServiceLocator.Get<EnemyPool>(),
                ServiceLocator.Get<BulletSystem>()));

            ServiceLocator.Bind<InputManager>(new InputManager(_inputSchema));

            ServiceLocator.Bind<GameListenersController>(_listenersController);
        }
    }
}