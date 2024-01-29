using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [NonSerialized] public bool IsPlayer;
        [NonSerialized] public int Damage;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collision2DObserver _collisionObserver;

        public Rigidbody2D Rigidbody => _rigidbody2D;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public event Action<Bullet, Collision2D> CollisionHappened;

        private void Start() => _collisionObserver.CollisionEntered += OnCollision;

        private void OnDestroy() => _collisionObserver.CollisionExited -= OnCollision;

        private void OnCollision(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            CollisionHappened?.Invoke(this, collision);
        }


        private void DealDamage(GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
                return;

            if (IsPlayer == team.IsPlayer)
                return;

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(Damage);
        }
    }
}