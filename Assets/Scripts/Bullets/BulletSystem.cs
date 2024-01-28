using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public struct BulletSystemArgs
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Color;
        public int PhysicsLayer;
        public int Damage;
        public bool IsPlayer;
    }

    public sealed class BulletSystem : IFixedTickable, IBulletSystem
    {
        private readonly List<Bullet> _cache = new();
        private readonly Dictionary<Bullet, Vector2> _frozenBullets = new();
        private LevelBounds _levelBounds;
        private IBulletPool _bulletPool;
        private bool _isEnabled;

        [Inject]
        public BulletSystem(LevelBounds levelBounds, IBulletPool bulletPool)
        {
            _levelBounds = levelBounds;
            _bulletPool = bulletPool;
            _isEnabled = true;
        }

        public void FixedTick()
        {
            if (!_isEnabled)
                return;

            _cache.Clear();
            _cache.AddRange(_bulletPool.ActiveBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                Bullet bullet = _cache[i];

                if (!_levelBounds.IsInBounds(bullet.transform.position))
                {
                    Debug.Break();
                    MoveBulletToPool(bullet);
                }
            }
        }

        public void OnPause() => FreezeBullets();

        public void OnResume() => UnfreezeBullets();

        public void OnGameEnd() => FreezeBullets();

        public void FlyBulletByArgs(BulletSystemArgs bulletSystemArgs)
        {
            if (!_isEnabled)
                return;

            _bulletPool.SpawnBullet(bulletSystemArgs).CollisionHappened += OnBulletCollision;
        }

        private void FreezeBullets()
        {
            _isEnabled = false;

            foreach (Bullet activeBullet in _bulletPool.ActiveBullets)
            {
                _frozenBullets.Add(activeBullet, activeBullet.Rigidbody.velocity);
                activeBullet.Rigidbody.velocity = Vector2.zero;
            }
        }

        private void UnfreezeBullets()
        {
            _isEnabled = true;

            foreach (Bullet frozenBullet in _frozenBullets.Keys)
                frozenBullet.Rigidbody.velocity = _frozenBullets[frozenBullet];

            _frozenBullets.Clear();
        }

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