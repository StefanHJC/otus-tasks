using System;
using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public class GameLauncher : IDisposable, IGameStartInvoker
    {
        private readonly UIMediator _uiMeddiator;
        private readonly ButtonClickListener _buttonListener;

        public event Action OnGameStarted;

        [Inject]
        public GameLauncher(UIMediator uiMediator, ButtonClickListener buttonListener)
        {
            _uiMeddiator = uiMediator;
            _buttonListener = buttonListener;

            _buttonListener.OnStartFired += StartGameAsync;
        }

        public async void StartGameAsync() => await AwaitGameStartAsync(delayInSeconds: 3);

        public void Dispose() => _buttonListener.OnStartFired -= StartGameAsync;

        private async Task AwaitGameStartAsync(int delayInSeconds)
        {
            int i = 0;

            while (i <= delayInSeconds)
            {
                _uiMeddiator.ShowScreenText((delayInSeconds - i++).ToString());
               
                await Task.Delay(millisecondsDelay: 1000);
            }
            _uiMeddiator.HideScreenText();
            OnGameStarted?.Invoke();
        }
    }

    public interface IGameStartInvoker
    {
        event Action OnGameStarted;
    }
}