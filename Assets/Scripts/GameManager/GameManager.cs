using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : IService
    {
        private const int GameStartDelay = 3;

        private readonly GameListenersController _gameListenersController;
        private readonly AssetProvider _assets;
        private readonly Transform _characterPosition;
        private readonly CharacterProvider _characterProvider;
        private readonly UnitView _characterView;
        private readonly HUD _hud;

        public GameManager(GameListenersController gameListenersController, AssetProvider assets, 
            HUD hud, Transform characterPosition, CharacterProvider provider, UnitView characterView)
        {
            _gameListenersController = gameListenersController;
            _assets = assets;
            _hud = hud;
            _characterPosition = characterPosition;
            _characterProvider = provider;
            _characterView = characterView;

            _hud.StartButton.Clicked += StartGameAsync;
            _hud.PauseButton.Clicked += PauseGame;
            _hud.ResumeButton.Clicked += ResumeGame;
        }

        public void PauseGame() => _gameListenersController.Pause();

        public void ResumeGame() => _gameListenersController.Resume();

        public async void StartGameAsync()
        {
            await SetGameStartDelayAsync(delayInSeconds: GameStartDelay);

            InstallGameSessionBindings(InstantiateCharacterView(at: _characterPosition, prefab: _characterView));
            _gameListenersController.StartGame();
            _hud.PauseButton.Enable();
        }

        public void FinishGame()
        {
            _hud.ScreenTextRenderer.Enable();
            _hud.PauseButton.Disable();
            _hud.ResumeButton.Disable();

            _hud.ScreenTextRenderer.Text = "Game over!";
            _gameListenersController.Pause();
            _characterProvider.Character.View.Disable();
        }

        private async Task SetGameStartDelayAsync(int delayInSeconds)
        {
            int i = 0;
            _hud.ScreenTextRenderer.Enable();

            while (i <= delayInSeconds)
            {
                _hud.ScreenTextRenderer.Text = (delayInSeconds - i++).ToString();
                await Task.Delay(millisecondsDelay: 1000);
            }
            _hud.ScreenTextRenderer.Disable();
        }

        private void InstallGameSessionBindings(UnitView view)
        {
            ServiceLocator.Bind<PlayerDeathListener>(new PlayerDeathListener(view.GetComponent<HitPointsComponent>()));

            ServiceLocator.Bind<CharacterController>(new CharacterController(
                ServiceLocator.Get<InputManager>(),
                ServiceLocator.Get<BulletSystem>(),
                view));
            _characterProvider.Character = ServiceLocator.Get<CharacterController>();
        }

        private UnitView InstantiateCharacterView(Transform at, UnitView prefab)
        {
            UnitView unitViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;

            return unitViewInstance;
        }
    }

    public class PlayerInstaller : IService
    {
        private readonly CharacterProvider _characterProvider;
        private readonly AssetProvider _assets;
        private readonly UnitView _prefab;
        private readonly Transform _position;

        public PlayerInstaller(CharacterProvider characterProvider, AssetProvider assets, UnitView prefab, Transform position)
        {
            _characterProvider = characterProvider;
            _assets = assets;
            _prefab = prefab;
            _position = position;
        }

        public void InstallGameSessionBindings(UnitView view)
        {
            ServiceLocator.Bind<PlayerDeathListener>(new PlayerDeathListener(view.GetComponent<HitPointsComponent>()));

            ServiceLocator.Bind<CharacterController>(new CharacterController(
                ServiceLocator.Get<InputManager>(),
                ServiceLocator.Get<BulletSystem>(),
                view));
            _characterProvider.Character = ServiceLocator.Get<CharacterController>();
        }

        public UnitView InstantiateCharacterView()
        {
            UnitView unitViewInstance = _assets.Instantiate(_prefab);
            unitViewInstance.transform.position = _position.position;
            unitViewInstance.transform.rotation = _position.rotation;

            return unitViewInstance;
        }
    }
}