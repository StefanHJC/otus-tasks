using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletBuilder
    {
        Bullet BulletInstance { get; }
        BulletBuilder BuildBullet();
        BulletBuilder SetVelocity(Vector2 velocity);
        BulletBuilder SetPhysicsLayer(int physicsLayer);
        BulletBuilder SetPosition(Vector3 position);
        BulletBuilder SetParent(Transform parent);
        BulletBuilder SetColor(Color color);
    }
}