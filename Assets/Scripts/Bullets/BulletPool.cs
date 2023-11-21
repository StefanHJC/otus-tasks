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
                SetPosition(args.Position).
                SetColor(args.Color).
                SetPhysicsLayer(args.PhysicsLayer).
                SetVelocity(args.Velocity).
                BulletInstance;

            _builder.BulletInstance.damage = args.Damage;
            _builder.BulletInstance.isPlayer = args.IsPlayer;
            _activeBullets.Add(bullet);

            return bullet;
        }

        private Bullet GetFromPool(BulletSystem.Args args)
        {
            Bullet bullet = _bulletPool.Dequeue();

            bullet.transform.SetParent(_worldTransform);
            bullet.SpriteRenderer.color = args.Color;
            bullet.gameObject.layer = args.PhysicsLayer;
            bullet.transform.position = args.Position;
            bullet.Rigidbody.velocity = args.Velocity;

            bullet.damage = args.Damage;
            bullet.isPlayer = args.IsPlayer;

            return bullet;
        }
    }
}