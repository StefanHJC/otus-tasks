using Zenject;

namespace ShootEmUp
{
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
}