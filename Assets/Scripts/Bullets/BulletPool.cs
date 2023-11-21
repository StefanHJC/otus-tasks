using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;
        
        [Space]
        [SerializeField] private Transform _container;
        [SerializeField] private BulletBuilder _builder;
        [SerializeField] private Transform _worldTransform;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();

        public IReadOnlyCollection<Bullet> ActiveBullets => _activeBullets;

        public Bullet SpawnBullet(BulletSystem.Args args)
        {
            Bullet bullet = _bulletPool.Count > 0 ? GetFromPool(args) : BuildNew(args);

            _activeBullets.Add(bullet);

            return bullet;
        }

        public bool TryUnspawnBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(_container);
                _bulletPool.Enqueue(bullet);

                return true;
            }
            return false;
        }

        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                Bullet bullet = _builder.
                    BuildBullet().
                    SetPosition(_container.position).
                    SetParent(_container).
                    BulletInstance;

                _bulletPool.Enqueue(bullet);
            }
        }

        private Bullet BuildNew(BulletSystem.Args args)
        {
            Bullet bullet = _builder.
                BuildBullet().
                SetParent(_worldTransform).
                SetPosition(args.position).
                SetColor(args.color).
                SetPhysicsLayer(args.physicsLayer).
                SetVelocity(args.velocity).
                BulletInstance;

            _builder.BulletInstance.damage = args.damage;
            _builder.BulletInstance.isPlayer = args.isPlayer;
            _activeBullets.Add(bullet);

            return bullet;
        }

        private Bullet GetFromPool(BulletSystem.Args args)
        {
            Bullet bullet = _bulletPool.Dequeue();

            bullet.transform.SetParent(_worldTransform);
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