using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : IService, IFixedUpdateListener
    {
        private readonly InputManager _inputManager;
        private readonly BulletSystem _bulletSystem;
        private readonly UnitView _view;

        public CharacterController(InputManager inputManager, BulletSystem bulletSystem, UnitView view)
        {
            _inputManager = inputManager;
            _bulletSystem = bulletSystem;
            _view = view;

            _inputManager.AttackActionPerformed += OnFlyBullet;
        }

        public void OnFixedUpdate()
        {
            Debug.Log(_inputManager.HorizontalDirection.ToString());
            _view.Movement.Move(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(_view.Weapon.GetBulletArgs(Vector2.zero));
    }
}