using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletBuilder : IService
    {
        private readonly AssetProvider _assetProvider;
        private readonly Bullet _prefab;
        private Bullet _bulletInstance;

        public Bullet BulletInstance => _bulletInstance ??= GetBullet();

        public BulletBuilder(Bullet prefab, AssetProvider assetProvider)
        {
            _prefab = prefab;
            _assetProvider = assetProvider;
        }

        public BulletBuilder BuildBullet()
        {
            _bulletInstance = GetBullet();

            return this;
        }

        public BulletBuilder SetVelocity(Vector2 velocity)
        {
            BulletInstance.Rigidbody.velocity = velocity;

            return this;
        }

        public BulletBuilder SetPhysicsLayer(int physicsLayer)
        {
            BulletInstance.gameObject.layer = physicsLayer;

            return this;
        }

        public BulletBuilder SetPosition(Vector3 position)
        {
            BulletInstance.transform.position = position;

            return this;
        }
        
        public BulletBuilder SetParent(Transform parent)
        {
            BulletInstance.transform.parent = parent;

            return this;
        }

        public BulletBuilder SetColor(Color color)
        {
            BulletInstance.SpriteRenderer.color = color;

            return this;
        }

        private Bullet GetBullet() => _assetProvider.Instantiate(_prefab);
    }
}