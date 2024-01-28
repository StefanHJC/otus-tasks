using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public class UIMediator
    {
        private IHUD _hud;

        [Inject]
        public UIMediator(UIProvider provider)
        {
            LazyInitAsync(provider);
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

        private async void LazyInitAsync(UIProvider provider)
        {
            while (provider.Hud == null)
                await Task.Yield();

            _hud = provider.Hud;
        }
    }
}