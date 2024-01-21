using Zenject;

namespace ShootEmUp
{
    public class ZenjectTickableInterruptionController : IGameInterruptionController
    {
        private readonly TickableManager _tickableManager;
        private readonly IBulletSystem _bulletSystem;

        [Inject]
        public ZenjectTickableInterruptionController(TickableManager tickableManager, IBulletSystem bulletSystem)
        {
            _tickableManager = tickableManager;
            _bulletSystem = bulletSystem;
        }

        public void Pause()
        {
            _tickableManager.IsPaused = true;
            _bulletSystem.OnPause();
        }

        public void Resume()
        {
            _tickableManager.IsPaused = false;
            _bulletSystem.OnResume();
        }
    }
}