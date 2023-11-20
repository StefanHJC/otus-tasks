using UnityEngine;

namespace ShootEmUp
{
    public class BulletBuilder : MonoBehaviour
    {
        [SerializeField]
        private Bullet prefab;
        
        private Bullet bulletInstance;

        public Bullet BulletInstance => bulletInstance ??= GetBullet();

        public BulletBuilder BuildBullet()
        {
            bulletInstance = GetBullet();

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

        private Bullet GetBullet() => Object.Instantiate(prefab);
    }
}