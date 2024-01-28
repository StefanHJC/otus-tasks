using System;
using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public class ButtonClickListener : IDisposable
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
            LazyInitAsync(provider); // ��������, ��� �� �������, ����� �� � �� ������ �������� �������� ������ UIFactory ��� �� ��������������� HUD(��� ��������� ��)
        }

        public void Dispose()
        {
            _startButton.Clicked -= InvokeStartButtonEvent;
            _resumeButton.Clicked -= InvokePauseButtonEvent;
            _pauseButton.Clicked -= InvokeResumeButtonEvent;
        }

        private async void LazyInitAsync(UIProvider provider)
        {
            while (provider.Hud == null)
                await Task.Yield();

            _startButton = provider.Hud.StartButton;
            _resumeButton = provider.Hud.StartButton;
            _pauseButton = provider.Hud.StartButton;

            _startButton.Clicked += InvokeStartButtonEvent;
            _resumeButton.Clicked += InvokePauseButtonEvent;
            _pauseButton.Clicked += InvokeResumeButtonEvent;
        }

        private void InvokeStartButtonEvent() => OnStartFired?.Invoke();

        private void InvokeResumeButtonEvent() => OnResumeFired?.Invoke();

        private void InvokePauseButtonEvent() => OnPauseFired?.Invoke();
    }
}