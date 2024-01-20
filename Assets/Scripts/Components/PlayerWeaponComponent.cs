using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerWeaponComponent : WeaponComponent
    {
        public override BulletSystemArgs GetBulletArgs(Vector2 _)
        {
            BulletSystemArgs bulletArgs = base.GetBulletArgs(_);

            bulletArgs.Velocity = Rotation * Vector3.up * Config.Speed;
            bulletArgs.IsPlayer = true;

            return bulletArgs;
        }
    }
}