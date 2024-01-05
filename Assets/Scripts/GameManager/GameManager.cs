using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : IService
    {
        private readonly GameListenersController _gameListenersController;
        private readonly AssetProvider _assets;
        private readonly Transform _characterPosition;
        private readonly CharacterController _character;
        private readonly HUD _hud;
        private readonly CharacterProvider _characterProvider;
        private readonly UnitView _characterView;

        public GameManager(GameListenersController gameListenersController, AssetProvider assets, 
            HUD hud, Transform characterPosition, CharacterProvider provider, UnitView characterView)
        {
            _gameListenersController = gameListenersController;
            _assets = assets;
            _hud = hud;
            _characterPosition = characterPosition;
            _characterProvider = provider;
            _characterView = characterView;

            _hud.StartButton.Clicked += StartGame;
            _hud.PauseButton.Clicked += PauseGame;
            _hud.ResumeButton.Clicked += ResumeGame;
        }

        public void PauseGame() => _gameListenersController.Pause();

        public void ResumeGame() => _gameListenersController.Resume();

        public async void StartGame()
        {
            //TODO start pre game counting

            InstallGameSessionBindings(InstantiateCharacterView(at: _characterPosition, prefab: _characterView));
        }

        private void InstallGameSessionBindings(UnitView view)
        {
            ServiceLocator.Bind<GameEndListener>(new GameEndListener(view.GetComponent<HitPointsComponent>(),
                ServiceLocator.Get<GameManager>()));

            ServiceLocator.Bind<CharacterController>(new CharacterController(
                ServiceLocator.Get<InputManager>(),
                ServiceLocator.Get<BulletSystem>(),
                view));
            _characterProvider.Character = ServiceLocator.Get<CharacterController>();
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private UnitView InstantiateCharacterView(Transform at, UnitView prefab)
        {
            UnitView unitViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;

            return unitViewInstance;
        }
    }
}