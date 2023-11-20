using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
        [SerializeField] private Transform container;
        //[SerializeField] private Bullet prefab;
        [SerializeField] private BulletBuilder _builder;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = _builder.
                            BuildBullet().
                            SetPosition(this.container.position).
                            SetParent(this.container).
                            BulletInstance;

                this.m_bulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                var bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.MoveBulletToPool(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
                GetBulletFromPoolAndSetUp(args, bullet);
            else
                BuildNewBulletAndSetUp(args);

            this.m_activeBullets.Add(bullet);
        }

        private void BuildNewBulletAndSetUp(Args args)
        {
            Bullet bullet = _builder.
                            BuildBullet().
                            SetParent(this.worldTransform).
                            SetPosition(args.position).
                            SetColor(args.color).
                            SetPhysicsLayer(args.physicsLayer).
                            SetVelocity(args.velocity).
                            BulletInstance;

            _builder.BulletInstance.damage = args.damage;
            _builder.BulletInstance.isPlayer = args.isPlayer;

            bullet.OnHit += this.OnBulletCollision;
        }

        private void GetBulletFromPoolAndSetUp(Args args, Bullet bullet)
        {
            bullet.transform.SetParent(this.worldTransform);
            bullet.SpriteRenderer.color = args.color;
            bullet.gameObject.layer = args.physicsLayer;
            bullet.transform.position = args.position;
            bullet.Rigidbody.velocity = args.velocity;

            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;

            bullet.OnHit += this.OnBulletCollision;
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.MoveBulletToPool(bullet);
        }

        private void MoveBulletToPool(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.OnHit -= this.OnBulletCollision;
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
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