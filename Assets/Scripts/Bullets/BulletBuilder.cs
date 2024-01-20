using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletBuilder : IBulletBuilder
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Bullet _prefab;
        private Bullet _bulletInstance;

        public Bullet BulletInstance => _bulletInstance ??= GetBullet();

        [Inject]
        public BulletBuilder(Bullet prefab, IAssetProvider assetProvider)
        {
            _prefab = prefab;
            _assetProvider = assetProvider;
        }

        public IBulletBuilder BuildBullet()
        {
            _bulletInstance = GetBullet();

            return this;
        }

        public IBulletBuilder SetVelocity(Vector2 velocity)
        {
            BulletInstance.Rigidbody.velocity = velocity;

            return this;
        }

        public IBulletBuilder SetPhysicsLayer(int physicsLayer)
        {
            BulletInstance.gameObject.layer = physicsLayer;

            return this;
        }

        public IBulletBuilder SetPosition(Vector3 position)
        {
            BulletInstance.transform.position = position;

            return this;
        }
        
        public IBulletBuilder SetParent(Transform parent)
        {
            BulletInstance.transform.parent = parent;

            return this;
        }

        public IBulletBuilder SetColor(Color color)
        {
            BulletInstance.SpriteRenderer.color = color;

            return this;
        }

        private Bullet GetBullet() => _assetProvider.Instantiate(_prefab);
    }
}