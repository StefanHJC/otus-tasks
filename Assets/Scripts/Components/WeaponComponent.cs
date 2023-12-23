using UnityEngine;

namespace ShootEmUp
{
    public interface IWeaponComponent
    {
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        
        BulletSystem.Args GetBulletArgs(Vector2 targetPos);
    }

    public abstract class WeaponComponent : MonoBehaviour, IWeaponComponent
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