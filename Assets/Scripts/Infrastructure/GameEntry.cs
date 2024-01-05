using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [DefaultExecutionOrder(-9999)]
    public class GameEntry : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private UnitView _enemyPrefab;
        
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
        [SerializeField] private UnitView _unitView;

        [Space]
        [Header("Game listeners controller")]
        [SerializeField] private GameListenersController _listenersController;

        [Space]
        [Header("UI")]
        [SerializeField] private Canvas _hud;
        [SerializeField] private Button _start;
        [SerializeField] private Button _pause;
        [SerializeField] private Button _resume;
        [SerializeField] private TMP_Text _centerText;

        private CharacterProvider _characterProvider;

        private void Awake()
        {
            BindServices();
            StartGame();
        }
        
        private void BindServices()
        {
            ServiceLocator.Init(_listenersController);

            _characterProvider = new CharacterProvider();

            ServiceLocator.Bind<Scheduler>(new Scheduler());

            ServiceLocator.Bind<HUD>(InstantiateHUD());

            ServiceLocator.Bind<AssetProvider>(new AssetProvider());
            ServiceLocator.Bind<GameManager>(
                new GameManager(_listenersController, 
                ServiceLocator.Get<AssetProvider>(),
                ServiceLocator.Get<HUD>(),
                _characterStartPosition,
                _characterProvider,
                _unitView));

            ServiceLocator.Bind<BulletBuilder>(new BulletBuilder(_bulletPrefab, ServiceLocator.Get<AssetProvider>()));
            ServiceLocator.Bind<BulletPool>(new BulletPool(_bulletContainer, ServiceLocator.Get<BulletBuilder>(), _world));
            ServiceLocator.Bind<BulletSystem>(new BulletSystem(_levelBounds, ServiceLocator.Get<BulletPool>()));

            ServiceLocator.Bind<EnemyFactory>(new EnemyFactory(_enemyPrefab, _listenersController,ServiceLocator.Get<AssetProvider>()));
            ServiceLocator.Bind<EnemyPool>(new EnemyPool(_enemyContainer, _world, ServiceLocator.Get<EnemyFactory>()));
            ServiceLocator.Bind<EnemyManager>(new EnemyManager(_enemyPositions, _characterProvider, 
                ServiceLocator.Get<EnemyPool>(),
                ServiceLocator.Get<BulletSystem>()));

            ServiceLocator.Bind<InputManager>(new InputManager(_inputSchema));

            ServiceLocator.Bind<GameListenersController>(_listenersController);
        }

        private HUD InstantiateHUD()
        {
            return new HUD(
                startButton: new GameplayButton(_start),
                resumeButton: new GameplayButton(_resume),
                pauseButton: new GameplayButton(_pause),
                screenTextRenderer: new ScreenTextRenderer(_centerText));
        }
    }
}