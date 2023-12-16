using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : IService, IFixedUpdateListener
    {
        private readonly List<Bullet> _cache = new();
        private LevelBounds _levelBounds;
        private BulletPool _bulletPool;

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }

        public BulletSystem(LevelBounds levelBounds, BulletPool bulletPool)
        {
            _levelBounds = levelBounds;
            _bulletPool = bulletPool;
        }

        public void OnFixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_bulletPool.ActiveBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                Bullet bullet = _cache[i];

                if (!_levelBounds.IsInBounds(bullet.transform.position))
                {
                    MoveBulletToPool(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args) => _bulletPool.SpawnBullet(args).CollisionHappened += OnBulletCollision;

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            MoveBulletToPool(bullet);
        }

        private void MoveBulletToPool(Bullet bullet)
        {
            if (_bulletPool.TryUnspawnBullet(bullet)) 
                bullet.CollisionHappened -= OnBulletCollision;
        }
    }
}