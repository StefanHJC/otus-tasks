using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager
    {
        private const int GameStartDelay = 3;

        private readonly Transform _characterPosition;
        private readonly UnitView _characterView;
        private readonly IAssetProvider _assets;
        private readonly IHUD _hud;

        [Inject]
        public GameManager(IAssetProvider assets, IHUD hud, Transform characterPosition, UnitView characterView)
        {
            _assets = assets;
            _hud = hud;
            _characterPosition = characterPosition;
            _characterView = characterView;

            //_hud.StartButton.Clicked += StartGameAsync;
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
            //_character.View.Disable();
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
        private readonly UIMediator _uiMeddiator;

        [Inject]
        public GameLauncher(UIMediator uiMediator)
        {
            _uiMeddiator = uiMediator;
        }

        public async void StartGameAsync()
        {
            await AwaitGameStartAsync(delayInSeconds: 3);

            //InstallGameSessionBindings(InstantiateCharacterView(at: _characterPosition, prefab: _characterView));
            //_gameListenersController.StartGame();
            //_hud.PauseButton.Enable();
        }

        private async Task AwaitGameStartAsync(int delayInSeconds)
        {
            int i = 0;

            while (i <= delayInSeconds)
            {
                _uiMeddiator.ShowScreenText((delayInSeconds - i++).ToString());
               
                await Task.Delay(millisecondsDelay: 1000);
            }
            _uiMeddiator.HideScreenText();
        }
    }
}