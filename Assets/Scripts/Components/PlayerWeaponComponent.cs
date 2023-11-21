using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerWeaponComponent : WeaponComponent
    {
        public override BulletSystem.Args GetBulletArgs(Vector2 _)
        {
            BulletSystem.Args bulletArgs = base.GetBulletArgs(_);

            bulletArgs.Velocity = Rotation * Vector3.up * Config.Speed;
            bulletArgs.IsPlayer = true;

            return bulletArgs;
        }
    }
}