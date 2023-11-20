using UnityEngine;

namespace ShootEmUp
{
    public class MovementObserver : MonoBehaviour
    {
        private EnemyMoveAgent moveAgent;

        public bool IsMoving => this.moveAgent != null && !moveAgent.IsReached;

        private void Start()
        {
            if (TryGetComponent<EnemyMoveAgent>(out var moveAgent))
                this.moveAgent = moveAgent;
        }
    }
}