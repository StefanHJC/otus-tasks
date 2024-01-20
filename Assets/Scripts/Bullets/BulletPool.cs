using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletPool : IBulletPool
    {
        private const int InitialCount = 50;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private Transform _container;
        private Transform _worldTransform;
        private IBulletBuilder _builder;

        public IReadOnlyCollection<Bullet> ActiveBullets => _activeBullets;

        [Inject]
        public BulletPool(Transform container, IBulletBuilder builder, Transform world)
        {
            _container = container;
            _builder = builder;
            _worldTransform = world;

            Init();
        }

        private void Init()
        {
            for (var i = 0; i < InitialCount; i++)
            {
                Bullet bullet = _builder.BuildBullet().SetPosition(_container.position).SetParent(_container).BulletInstance;

                _bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet(BulletSystemArgs args)
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

        private Bullet BuildNew(BulletSystemArgs args)
        {
            Bullet bullet = _builder.
                BuildBullet().
                SetParent(_worldTransform).
                SetPosition(args.Position).
                SetColor(args.Color).
                SetPhysicsLayer(args.PhysicsLayer).
                SetVelocity(args.Velocity).
                BulletInstance;

            _builder.BulletInstance.Damage = args.Damage;
            _builder.BulletInstance.IsPlayer = args.IsPlayer;
            _activeBullets.Add(bullet);

            return bullet;
        }

        private Bullet GetFromPool(BulletSystemArgs args)
        {
            Bullet bullet = _bulletPool.Dequeue();

            bullet.transform.SetParent(_worldTransform);
            bullet.SpriteRenderer.color = args.Color;
            bullet.gameObject.layer = args.PhysicsLayer;
            bullet.transform.position = args.Position;
            bullet.Rigidbody.velocity = args.Velocity;

            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;

            return bullet;
        }
    }
}