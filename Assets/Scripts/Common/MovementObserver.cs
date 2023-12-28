
namespace ShootEmUp
{
    public sealed class MovementObserver 
    {
        private readonly EnemyMoveAgent _moveAgent;

        public bool IsMoving => !_moveAgent.IsReached;

        public MovementObserver(EnemyMoveAgent moveAgent)
        {
            _moveAgent = moveAgent;
        }
    }
}