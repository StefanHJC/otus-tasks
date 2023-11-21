using UnityEngine;

namespace ShootEmUp
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;

        [SerializeField] protected BulletConfig Config;

        public Vector2 Position => _firePoint.position;

        public Quaternion Rotation => _firePoint.rotation;

        public virtual BulletSystem.Args GetBulletArgs(Vector2 targetPos) =>
            new BulletSystem.Args()
            {
                Position = Position,
                Color = Config.Color,
                PhysicsLayer = (int)Config.PhysicsLayer,
                Damage = Config.Damage,
            };
    }
}