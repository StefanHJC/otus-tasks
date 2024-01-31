using System;
using System.Linq;
using System.Threading.Tasks;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameLauncher : IDisposable, IGameStartListener
    {
        private readonly UIMediator _uiMeddiator;
        private readonly ButtonClickListener _buttonListener;
        private readonly int _gameStartDelay;

        public event Action OnGameStarted;

        [Inject]
        public GameLauncher(UIMediator uiMediator, ButtonClickListener buttonListener, IDatabaseService data)
        {
            _uiMeddiator = uiMediator;
            _buttonListener = buttonListener;
            _gameStartDelay = data.Get<GameStaticData>().FirstOrDefault().GameStartDelayInSeconds;

            _buttonListener.OnStartFired += StartGameAsync;
        }

        public async void StartGameAsync() => await AwaitGameStartAsync(delayInSeconds: _gameStartDelay);

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
}