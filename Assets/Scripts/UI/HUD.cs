
using System;

namespace ShootEmUp
{
    public class HUD : IService, IDisposable
    {
        public readonly GameplayButton StartButton;
        public readonly GameplayButton ResumeButton;
        public readonly GameplayButton PauseButton;
        public readonly ScreenTextRenderer ScreenTextRenderer;

        public HUD(GameplayButton startButton, GameplayButton resumeButton, GameplayButton pauseButton, ScreenTextRenderer screenTextRenderer)
        {
            StartButton = startButton;
            ResumeButton = resumeButton;
            PauseButton = pauseButton;
            ScreenTextRenderer = screenTextRenderer;

            StartButton.Clicked += OnStartFired;
            ResumeButton.Clicked += OnResumeFired;
            PauseButton.Clicked += OnPauseFired;
        }

        public void Dispose()
        {
            StartButton.Clicked -= OnStartFired;
            ResumeButton.Clicked -= OnResumeFired;
            PauseButton.Clicked -= OnPauseFired;
        }

        private void OnStartFired() => StartButton.Disable();

        private void OnResumeFired()
        {
            ResumeButton.Disable();
            PauseButton.Enable();
        }

        private void OnPauseFired()
        {
            PauseButton.Disable();
            ResumeButton.Enable();
        }
    }
}