using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private BulletPool _bulletPool;

        private readonly List<Bullet> _cache = new();

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }

        private void FixedUpdate()
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