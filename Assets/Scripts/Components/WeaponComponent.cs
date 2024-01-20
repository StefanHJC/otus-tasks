using UnityEngine;

namespace ShootEmUp
{
    public interface IWeaponComponent
    {
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        
        BulletSystemArgs GetBulletArgs(Vector2 targetPos);
    }

    public abstract class WeaponComponent : MonoBehaviour, IWeaponComponent
    {
        [SerializeField] private Transform _firePoint;

        [SerializeField] protected BulletConfig Config;

        public Vector2 Position => _firePoint.position;

        public Quaternion Rotation => _firePoint.rotation;

        public virtual BulletSystemArgs GetBulletArgs(Vector2 targetPos) =>
            new BulletSystemArgs()
            {
                Position = Position,
                Color = Config.Color,
                PhysicsLayer = (int)Config.PhysicsLayer,
                Damage = Config.Damage,
            };
    }
}