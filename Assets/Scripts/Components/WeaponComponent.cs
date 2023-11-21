using UnityEngine;

namespace ShootEmUp
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        [SerializeField] protected BulletConfig config;

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
                position = Position,
                color = config.Color,
                physicsLayer = (int)config.PhysicsLayer,
                damage = config.Damage,
            };
    }
}