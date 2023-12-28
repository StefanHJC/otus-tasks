using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : IService, IFixedUpdateListener
    {
        private readonly InputManager _inputManager;
        private readonly BulletSystem _bulletSystem;
        
        public UnitView View { get; private set; }

        public CharacterController(InputManager inputManager, BulletSystem bulletSystem, UnitView view)
        {
            _inputManager = inputManager;
            _bulletSystem = bulletSystem;
            View = view;

            _inputManager.AttackActionPerformed += OnFlyBullet;
        }

        public void OnFixedUpdate()
        {
            Debug.Log(_inputManager.HorizontalDirection.ToString());
            View.Movement.Move(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(View.Weapon.GetBulletArgs(Vector2.zero));
    }
}