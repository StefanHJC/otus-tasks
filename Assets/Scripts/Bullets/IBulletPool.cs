using System.Collections.Generic;

namespace ShootEmUp
{
    public interface IBulletPool
    {
        IReadOnlyCollection<Bullet> ActiveBullets { get; }
        Bullet SpawnBullet(BulletSystemArgs args);
        bool TryUnspawnBullet(Bullet bullet);
    }
}