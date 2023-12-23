using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent
    {
        private const float DestinationReachEpsilon = 0.25f;
        
        private readonly IMoveComponent _moveComponent;
        private readonly IUnitView _view;
        private Vector2 _destination;
        private bool _isReached;

        public bool IsReached => _isReached;

        public EnemyMoveAgent(IUnitView view)
        {
            _moveComponent = view.Movement;
            _view = view;
        }

        public void Update()
        {
            if (_isReached)
                return;

            Vector2 vector = _destination - _view.Position;
            
            if (vector.magnitude <= DestinationReachEpsilon)
            {
                _isReached = true;
                
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(direction);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
    }
}