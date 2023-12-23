using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveComponent
    {
        void Move(Vector2 vector);
    }

    public sealed class MoveComponent : MonoBehaviour, IMoveComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;
        
        public void Move(Vector2 vector)
        {
            Vector2 nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}