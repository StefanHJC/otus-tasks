using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : IFixedUpdateListener
    {
        private readonly InputManager _inputManager;
        private readonly BulletSystem _bulletSystem;
        private readonly CharacterView _view;

        public CharacterController(InputManager inputManager, BulletSystem bulletSystem, CharacterView view)
        {
            _inputManager = inputManager;
            _bulletSystem = bulletSystem;
            _view = view;

            _inputManager.AttackActionPerformed += OnFlyBullet;
        }

        public void OnFixedUpdate()
        {
            _view.Movement.MoveByRigidbodyVelocity(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(_view.Weapon.GetBulletArgs(Vector2.zero));
    }
}