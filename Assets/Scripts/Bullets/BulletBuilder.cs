using UnityEngine;

namespace ShootEmUp
{
    public class BulletBuilder : IService
    {
        private Bullet _prefab;
        private Bullet _bulletInstance;

        public Bullet BulletInstance => _bulletInstance ??= GetBullet();

        public BulletBuilder(Bullet prefab)
        {
            _prefab = prefab;
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

        private Bullet GetBullet() => AssetProvider.Instantiate(_prefab);
    }

    public static class AssetProvider
    {
        public static T Instantiate<T>(T prefab)  where T : Component => Object.Instantiate(prefab);
 
        public static GameObject Instantiate(GameObject prefab) => Object.Instantiate(prefab);

        public static void Destroy(GameObject gameObject) => Object.Destroy(gameObject);
    }
}