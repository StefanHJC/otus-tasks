using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletBuilder
    {
        Bullet BulletInstance { get; }
        IBulletBuilder BuildBullet();
        IBulletBuilder SetVelocity(Vector2 velocity);
        IBulletBuilder SetPhysicsLayer(int physicsLayer);
        IBulletBuilder SetPosition(Vector3 position);
        IBulletBuilder SetParent(Transform parent);
        IBulletBuilder SetColor(Color color);
    }
}