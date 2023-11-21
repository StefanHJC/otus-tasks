using UnityEngine;

namespace ShootEmUp
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        [SerializeField] protected BulletConfig Config;

        public Vector2 Position
        {
            get { return this.firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return this.firePoint.rotation; }
        }

        [SerializeField]
        private Transform firePoint;

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