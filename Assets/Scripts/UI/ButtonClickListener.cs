using System;
using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public sealed class ButtonClickListener : IDisposable
    {
        private GameplayButton _startButton;
        private GameplayButton _resumeButton;
        private GameplayButton _pauseButton;

        public event Action OnStartFired;
        public event Action OnResumeFired;
        public event Action OnPauseFired;

        [Inject]
        public ButtonClickListener(UIProvider provider)
        {
            LazyInitAsync(provider); // наверное, все же костыль, нужен тк к на момент создания инстанса класса UIFactory еще не инстанциировала HUD(как пофиксить хз)
        }

        public void Dispose()
        {
            _startButton.Clicked -= InvokeStartButtonEvent;
            _resumeButton.Clicked -= InvokeResumeButtonEvent;
            _pauseButton.Clicked -= InvokePauseButtonEvent;
        }

        private async void LazyInitAsync(UIProvider provider)
        {
            while (provider.Hud == null)
                await Task.Yield();

            _startButton = provider.Hud.StartButton;
            _resumeButton = provider.Hud.ResumeButton;
            _pauseButton = provider.Hud.PauseButton;

            _startButton.Clicked += InvokeStartButtonEvent;
            _resumeButton.Clicked += InvokeResumeButtonEvent;
            _pauseButton.Clicked += InvokePauseButtonEvent;
        }

        private void InvokeStartButtonEvent() => OnStartFired?.Invoke();

        private void InvokeResumeButtonEvent() => OnResumeFired?.Invoke();

        private void InvokePauseButtonEvent() => OnPauseFired?.Invoke();
    }
}