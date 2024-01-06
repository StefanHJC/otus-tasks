using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : IService, IFixedUpdateListener, IGamePauseListener, IGameResumeListener, IGameEndListener
    {
        private readonly List<Bullet> _cache = new();
        private LevelBounds _levelBounds;
        private BulletPool _bulletPool;
        private bool _isEnabled;

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
            _isEnabled = true;
        }

        public void OnFixedUpdate()
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
                    MoveBulletToPool(bullet);
                }
            }
        }

        public void OnPause() => StopBullets();

        public void OnResume() => MoveBullets();

        public void OnGameEnd() => StopBullets();

        public void FlyBulletByArgs(Args args) => _bulletPool.SpawnBullet(args).CollisionHappened += OnBulletCollision;

        private void StopBullets()
        {
            _isEnabled = false;

            foreach (Bullet activeBullet in _bulletPool.ActiveBullets)
                activeBullet.GetComponent<Rigidbody2D>()?.Sleep();
        }

        private void MoveBullets()
        {
            _isEnabled = true;

            foreach (Bullet activeBullet in _bulletPool.ActiveBullets)
                activeBullet.GetComponent<Rigidbody2D>()?.WakeUp();
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