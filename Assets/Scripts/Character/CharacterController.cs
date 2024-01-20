using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController : IFixedTickable
    {
        private readonly InputManager _inputManager;
        private readonly IBulletSystem _bulletSystem;
        private bool _isEnabled;
        
        public UnitView View { get; private set; }

        [Inject]
        public CharacterController(InputManager inputManager, IBulletSystem bulletSystem, UnitView view)
        {
            _inputManager = inputManager;
            _bulletSystem = bulletSystem;
            View = view;
            _isEnabled = true;

            _inputManager.AttackActionPerformed += OnFlyBullet;
        }

        public void FixedTick()
        {
            View.Movement.Move(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        public void OnPause() => _isEnabled = false;

        public void OnResume() => _isEnabled = true;

        private void OnFlyBullet()
        {
            if (!_isEnabled)
                return;

            _bulletSystem.FlyBulletByArgs(View.Weapon.GetBulletArgs(Vector2.zero));
        }
    }
}