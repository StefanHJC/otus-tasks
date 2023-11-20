using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerWeaponComponent : WeaponComponent
    {
        public override BulletSystem.Args GetBulletArgs(Vector2 _)
        {
            BulletSystem.Args bulletArgs = base.GetBulletArgs(_);

            bulletArgs.velocity = Rotation * Vector3.up * config.speed;
            bulletArgs.isPlayer = true;

            return bulletArgs;
        }
    }
}