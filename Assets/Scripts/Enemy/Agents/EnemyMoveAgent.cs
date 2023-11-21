using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        private const float DestinationReachedEpsilon = 0.25f;
        
        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;
        private bool _isReached;

        public bool IsReached => _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached)
                return;

            Vector2 vector = _destination - (Vector2)transform.position;
            
            if (vector.magnitude <= DestinationReachedEpsilon)
            {
                _isReached = true;
                
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}