using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private MoveComponent movement;
        [SerializeField] private WeaponComponent weapon;
        [SerializeField] private BulletSystem _bulletSystem;
        
        private void OnEnable() => inputManager.FireActionPerformed += OnFlyBullet;

        private void OnDisable() => inputManager.FireActionPerformed -= OnFlyBullet;

        private void FixedUpdate()
        {
            movement.MoveByRigidbodyVelocity(new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(weapon.GetBulletArgs(Vector2.zero));
    }
}