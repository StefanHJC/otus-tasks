
namespace ShootEmUp
{
    public class HUD : IService
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

        private void OnStartFired()
        {
            ResumeButton.Enable();
            StartButton.Disable();
        }

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