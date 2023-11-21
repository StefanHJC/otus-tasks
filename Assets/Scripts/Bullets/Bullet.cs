using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnHit;

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField] 
        private Collision2DObserver _collisionObserver;
        
        public Rigidbody2D Rigidbody => rigidbody2D;
        public SpriteRenderer SpriteRenderer => spriteRenderer;

        private void Start() => _collisionObserver.CollisionEntered += OnCollision;

        private void OnDestroy() => _collisionObserver.CollisionExited -= OnCollision;

        private void OnCollision(Collision2D collision) => this.OnHit?.Invoke(this, collision);
    }
}