using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
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

        public Vector2 GetDirectionToTarget(Vector2 targetPos) => (targetPos - Position).normalized;
    }
}