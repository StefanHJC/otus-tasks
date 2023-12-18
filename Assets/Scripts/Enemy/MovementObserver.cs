using UnityEngine;

namespace ShootEmUp
{
    public sealed class MovementObserver : MonoBehaviour
    {
        private EnemyMoveAgent _moveAgent;

        public bool IsMoving => _moveAgent != null && !_moveAgent.IsReached;

        private void Start()
        {
            if (TryGetComponent<EnemyMoveAgent>(out var moveAgent))
                _moveAgent = moveAgent;
        }
    }
}