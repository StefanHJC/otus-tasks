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
            _hud.ScreenTextRenderer.Enable();
            _hud.ScreenTextRenderer.Text = "Game over!";

            _gameListenersController.Pause();
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