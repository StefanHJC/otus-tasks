using Zenject;

namespace ShootEmUp
{
    public class HUD : IHUD
    {
        private readonly GameplayButton _startButton;
        private readonly GameplayButton _resumeButton;
        private readonly GameplayButton _pauseButton;
        private readonly ScreenTextRenderer _screenTextRenderer;

        public GameplayButton StartButton => _startButton;
        public GameplayButton ResumeButton => _resumeButton;
        public GameplayButton PauseButton => _pauseButton;
        public ScreenTextRenderer ScreenTextRenderer => _screenTextRenderer;

        public HUD(GameplayButton startButton, GameplayButton resumeButton, GameplayButton pauseButton, ScreenTextRenderer screenTextRenderer)
        {
            _startButton = startButton;
            _resumeButton = resumeButton;
            _pauseButton = pauseButton;
            _screenTextRenderer = screenTextRenderer;

            StartButton.Clicked += OnStartFired;
            ResumeButton.Clicked += OnResumeFired;
            PauseButton.Clicked += OnPauseFired;
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