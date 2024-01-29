using System;
using System.Linq;
using System.Numerics;
using Zenject;

namespace ShootEmUp
{
    public class CharacterProvider
    {
        public CharacterController Character { get; set; }
        public HitPointsComponent CharacterHealth { get; set; }
    }

    public class CharacterController
    {
        private readonly CharacterAttackController _attackController;
        private readonly CharacterMoveController _moveController;
        private readonly IUnitView _view;

        public IUnitView View => _view;

        [Inject]
        public CharacterController(CharacterAttackController attackController, CharacterMoveController moveController, IUnitView view)
        {
            _attackController = attackController;
            _moveController = moveController;
            _view = view;

            _moveController.View = _view;
            _attackController.View = _view;
        }
    }

    public class CharacterFactory
    {
        private readonly CharacterProvider _provider;
        private readonly UnitStaticData.UnitPrefabData _data;
        private readonly DiContainer _di;
        private readonly IAssetProvider _assetProvider;

        [Inject]
        public CharacterFactory(CharacterProvider provider, DiContainer container, IDatabaseService data, IAssetProvider assetProvider)
        {
            _provider = provider;
            _di = container;
            _assetProvider = assetProvider;
            _data = data.
                        Get<UnitStaticData>().
                        FirstOrDefault().
                        PrefabData.
                        First(prefab => prefab.TypeId == UnitTypeId.Player);
        }

        public CharacterController InstantiateCharacter(Vector3 at)
        {
            UnitView viewInstance = _assetProvider.Instantiate(_data.Prefab).GetComponent<UnitView>();
            CharacterController characterInstance = _di.Instantiate<CharacterController>(new[] { viewInstance });

            _provider.Character ??= characterInstance;
            _provider.CharacterHealth ??= viewInstance.GetComponent<HitPointsComponent>();

            return characterInstance;
        }
    }

    public class GameloopController : IDisposable
    {
        private readonly CharacterFactory _characterFactory;
        private readonly UIMediator _uiMediator;
        private readonly IGameInterruptionController _gameInterruptionController;
        private readonly IGameStartInvoker _gameStartInvoker;
        private readonly IGameEndInvoker _gameEndInvoker;
        private readonly IEnemyManager _enemyManager;

        public GameloopController(IGameStartInvoker gameStartInvoker, IGameEndInvoker gameEndInvoker, CharacterFactory characterFactory, 
                                IEnemyManager enemyManager, UIMediator uiMediator, IGameInterruptionController gameInterruptionController)
        {
            _gameStartInvoker = gameStartInvoker;
            _gameEndInvoker = gameEndInvoker;
            _characterFactory = characterFactory;
            _enemyManager = enemyManager;
            _uiMediator = uiMediator;
            _gameInterruptionController = gameInterruptionController;

            _gameStartInvoker.OnGameStarted += OnGameStart;
            _gameEndInvoker.OnGameEnded += OnGameEnd;
        }

        public void Dispose()
        {
            _gameStartInvoker.OnGameStarted -= OnGameStart;
            _gameEndInvoker.OnGameEnded -= OnGameEnd;
        }

        private void OnGameStart()
        {
            _characterFactory.InstantiateCharacter(at: default);
            _enemyManager.Initialize();
            _uiMediator.ShowPauseButton();

            _gameStartInvoker.OnGameStarted -= OnGameStart;
        }

        private void OnGameEnd()
        {
            _uiMediator.ShowScreenText("Game over!!!");
            _uiMediator.HidePauseButton();
            _uiMediator.HideResumeButton();

            _gameInterruptionController.Pause();
        }
    }

    public interface IGameEndInvoker
    {
        event Action OnGameEnded;
    }
}