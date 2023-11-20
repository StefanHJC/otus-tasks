using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletPool bulletPool;

        private readonly List<Bullet> m_cache = new();

        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.bulletPool.ActiveBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                var bullet = this.m_cache[i];

                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.MoveBulletToPool(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args) => bulletPool.SpawnBullet(args).OnHit += this.OnBulletCollision;

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.MoveBulletToPool(bullet);
        }

        private void MoveBulletToPool(Bullet bullet)
        {
            if (bulletPool.TryUnspawnBullet(bullet)) 
                bullet.OnHit -= this.OnBulletCollision;
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}