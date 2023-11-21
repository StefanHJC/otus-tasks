using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyWeaponComponent : WeaponComponent
    {
        public override BulletSystem.Args GetBulletArgs(Vector2 targetPos)
        {
            BulletSystem.Args bulletArgs = base.GetBulletArgs(targetPos);

            bulletArgs.Velocity = GetDirectionToTarget(targetPos) * Config.Speed;
            bulletArgs.IsPlayer = false;

            return bulletArgs;
        }

        private Vector2 GetDirectionToTarget(Vector2 targetPos) => (targetPos - Position).normalized;
    }
}