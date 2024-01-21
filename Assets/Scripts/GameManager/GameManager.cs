using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using static UnityEngine.UI.CanvasScaler;

namespace ShootEmUp
{
    public sealed class GameManager
    {
        private const int GameStartDelay = 3;

        private readonly IAssetProvider _assets;
        private readonly Transform _characterPosition;
        private readonly UnitView _characterView;
        private readonly IHUD _hud;

        [Inject]
        public GameManager(IAssetProvider assets, IHUD hud, Transform characterPosition, UnitView characterView)
        {
            _assets = assets;
            _hud = hud;
            _characterPosition = characterPosition;
            _characterView = characterView;

            _hud.StartButton.Clicked += StartGameAsync;
            _hud.PauseButton.Clicked += PauseGame;
            _hud.ResumeButton.Clicked += ResumeGame;
        }

        public void PauseGame()
        {
            //_gameListenersController.Pause();
        }

        public void ResumeGame()
        {
            // _gameListenersController.Resume();
        }


        public void FinishGame()
        {
            _hud.ScreenTextRenderer.Enable();
            _hud.PauseButton.Disable();
            _hud.ResumeButton.Disable();

            _hud.ScreenTextRenderer.Text = "Game over!";
            //_gameListenersController.Pause();
            _character.View.Disable();
        }


        private void InstallGameSessionBindings(UnitView view)
        {
            // ServiceLocator.Bind<GameEndListener>(new GameEndListener(view.GetComponent<HitPointsComponent>(),
            //     ServiceLocator.Get<GameManager>()));
            //
            // ServiceLocator.Bind<CharacterController>(new CharacterController(
            //     ServiceLocator.Get<InputManager>(),
            //     ServiceLocator.Get<BulletSystem>(),
            //     view));
            // _characterProvider.Character = ServiceLocator.Get<CharacterController>();
        }
    }

    public class GameLauncher
    {

        [Inject]
        public GameLauncher()
        {

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

        public async void StartGameAsync()
        {
            await SetGameStartDelayAsync(delayInSeconds: GameStartDelay);

            InstallGameSessionBindings(InstantiateCharacterView(at: _characterPosition, prefab: _characterView));
            //_gameListenersController.StartGame();
            _hud.PauseButton.Enable();
        }
    }

    public class CharacterProvider
    {

        public CharacterProvider()
        {

        }

        private UnitView InstantiateCharacter(Transform at, UnitView prefab)
        {
            UnitView unitViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;

            return unitViewInstance;
        }
    }

    public enum UnitTypeId
    {
        Player,
        Enemy
    }
}