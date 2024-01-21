using Zenject;

namespace ShootEmUp
{
    public class ZenjectTickableInterruptionController : IGameInterruptionController
    {
        private readonly TickableManager _tickableManager;

        [Inject]
        public ZenjectTickableInterruptionController(TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
        }

        public void Pause() => _tickableManager.IsPaused = true;

        public void Resume() => _tickableManager.IsPaused = false;
    }
}