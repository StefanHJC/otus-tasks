using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private MoveComponent _movement;
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private BulletSystem _bulletSystem;
        
        private void OnEnable() => _inputManager.FireActionPerformed += OnFlyBullet;

        private void OnDisable() => _inputManager.FireActionPerformed -= OnFlyBullet;

        private void FixedUpdate()
        {
            _movement.MoveByRigidbodyVelocity(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(_weapon.GetBulletArgs(Vector2.zero));
    }
}