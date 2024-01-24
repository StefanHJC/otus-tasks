using System;

namespace ShootEmUp
{
    public class ButtonClickListener : IDisposable
    {
        private readonly GameplayButton _startButton;
        private readonly GameplayButton _resumeButton;
        private readonly GameplayButton _pauseButton;

        public event Action OnStartFired;
        public event Action OnResumeFired;
        public event Action OnPauseFired;

        public ButtonClickListener(IHUD hud)
        {
            _startButton = hud.StartButton;
            _resumeButton = hud.StartButton;
            _pauseButton = hud.StartButton;

            _startButton.Clicked += InvokeStartButtonEvent;
            _resumeButton.Clicked += InvokePauseButtonEvent;
            _pauseButton.Clicked += InvokeResumeButtonEvent;
        }

        public void Dispose()
        {
            _startButton.Clicked -= InvokeStartButtonEvent;
            _resumeButton.Clicked -= InvokePauseButtonEvent;
            _pauseButton.Clicked -= InvokeResumeButtonEvent;
        }

        private void InvokeStartButtonEvent() => OnStartFired?.Invoke();

        private void InvokeResumeButtonEvent() => OnResumeFired?.Invoke();

        private void InvokePauseButtonEvent() => OnPauseFired?.Invoke();
    }
}