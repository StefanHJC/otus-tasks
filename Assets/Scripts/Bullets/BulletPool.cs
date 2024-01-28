using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletPool : IBulletPool
    {
        private const int InitialCount = 50;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly LevelProvider _provider;
        private readonly IBulletBuilder _builder;
        private Transform _container;
        private Transform _worldTransform;

        public IReadOnlyCollection<Bullet> ActiveBullets => _activeBullets;

        [Inject]
        public BulletPool(IBulletBuilder builder, LevelProvider provider)
        {
            _builder = builder;
            _provider = provider;

            LazyInitAsync(provider); // наверное, все же костыль, нужен тк к на момент создания инстанса класса LevelFactory еще не инстанциировала Level(как пофиксить хз)
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

        private async void LazyInitAsync(LevelProvider provider)
        {
            while (provider.Level == null)
                await Task.Yield();

            _container = _provider.Level.BulletParent;
            _worldTransform = _provider.Level.Root;

            for (var i = 0; i < InitialCount; i++)
            {
                Bullet bullet = _builder.
                                    BuildBullet().
                                    SetPosition(_container.position).
                                    SetParent(_container).BulletInstance;

                _bulletPool.Enqueue(bullet);
            }
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