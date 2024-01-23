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
        private readonly IAssetProvider _assets;

        public CharacterProvider(IAssetProvider assets)
        {
            _assets = assets;
        }

        private UnitView InstantiateCharacter(Transform at, UnitView prefab)
        {
            UnitView unitViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;

            return unitViewInstance;
        }
    }

    public class UIMediator
    {
        private readonly IHUD _hud;

        [Inject]
        public UIMediator(IHUD hud)
        {
            _hud = hud;
        }

        public void ShowScreenText(string text)
        {
            _hud.ScreenTextRenderer.Text = text;
            _hud.ScreenTextRenderer.Enable();
        }

        public void HideScreenText() => _hud.ScreenTextRenderer.Disable();

        public void ShowStartButton() => _hud.StartButton.Enable();

        public void HideStartButton() => _hud.StartButton.Disable();

        public void ShowResumeButton() => _hud.ResumeButton.Enable();

        public void HideResumeButton() => _hud.ResumeButton.Disable();

        public void ShowPauseButton() => _hud.PauseButton.Enable();

        public void HidePauseButton() => _hud.PauseButton.Disable();
    }

    public class ButtonClickObserver : IDisposable
    {
        private readonly GameplayButton _startButton;
        private readonly GameplayButton _resumeButton;
        private readonly GameplayButton _pauseButton;

        public event Action OnStartFired;
        public event Action OnResumeFired;
        public event Action OnPauseFired;

        public ButtonClickObserver(IHUD hud)
        {
            _startButton = hud.StartButton;
            _resumeButton = hud.StartButton;
            _pauseButton = hud.StartButton;

            _startButton.Clicked += InvokeStartButtonEvent;
            _resumeButton.Clicked += InvokePauseButtonEvent;
            _pauseButton.Clicked += InvokeResumeButtonEvent;
        }

        private void InvokeStartButtonEvent() => OnStartFired?.Invoke();

        private void InvokeResumeButtonEvent() => OnResumeFired?.Invoke();

        private void InvokePauseButtonEvent() => OnPauseFired?.Invoke();


        public void Dispose()
        {
            _startButton.Clicked -= InvokeStartButtonEvent;
            _resumeButton.Clicked -= InvokePauseButtonEvent;
            _pauseButton.Clicked -= InvokeResumeButtonEvent;
        }
    }
}