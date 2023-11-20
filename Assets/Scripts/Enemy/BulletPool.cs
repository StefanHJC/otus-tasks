using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private BulletBuilder builder;
        [SerializeField] private Transform worldTransform;

        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();

        public IReadOnlyCollection<Bullet> ActiveBullets => m_activeBullets;

        public Bullet SpawnBullet(BulletSystem.Args args)
        {
            Bullet bullet = m_bulletPool.Count > 0 ? GetFromPool(args) : BuildNew(args);

            m_activeBullets.Add(bullet);

            return bullet;
        }

        public bool TryUnspawnBullet(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);

                return true;
            }
            return false;
        }

        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = builder.
                    BuildBullet().
                    SetPosition(this.container.position).
                    SetParent(this.container).
                    BulletInstance;

                this.m_bulletPool.Enqueue(bullet);
            }
        }

        private Bullet BuildNew(BulletSystem.Args args)
        {
            Bullet bullet = builder.
                BuildBullet().
                SetParent(this.worldTransform).
                SetPosition(args.position).
                SetColor(args.color).
                SetPhysicsLayer(args.physicsLayer).
                SetVelocity(args.velocity).
                BulletInstance;

            builder.BulletInstance.damage = args.damage;
            builder.BulletInstance.isPlayer = args.isPlayer;
            m_activeBullets.Add(bullet);

            return bullet;
        }

        private Bullet GetFromPool(BulletSystem.Args args)
        {
            Bullet bullet = this.m_bulletPool.Dequeue();

            bullet.transform.SetParent(this.worldTransform);
            bullet.SpriteRenderer.color = args.color;
            bullet.gameObject.layer = args.physicsLayer;
            bullet.transform.position = args.position;
            bullet.Rigidbody.velocity = args.velocity;

            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;

            return bullet;
        }
    }
}