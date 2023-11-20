using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyWeaponComponent : WeaponComponent
    {
        public override BulletSystem.Args GetBulletArgs(Vector2 targetPos)
        {
            BulletSystem.Args bulletArgs = base.GetBulletArgs(targetPos);

            bulletArgs.velocity = GetDirectionToTarget(targetPos) * config.speed;
            bulletArgs.isPlayer = false;

            return bulletArgs;
        }

        private Vector2 GetDirectionToTarget(Vector2 targetPos) => (targetPos - Position).normalized;
    }
}