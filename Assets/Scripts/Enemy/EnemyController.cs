using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyController : IFixedTickable, IDisposable
    {
        private readonly TickableManager _tickableManager;
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly MovementObserver _movementObserver;

        public UnitView View { get; private set; }

        public event Action<BulletSystemArgs> FirePerformed;
        public event Action<EnemyController> Died;

        public EnemyController(UnitView view, HitPointsComponent hitPoints, TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
            _moveAgent = new EnemyMoveAgent(view);
            _movementObserver = new MovementObserver(_moveAgent);
            _attackAgent = new EnemyAttackAgent(view, _movementObserver);
            _tickableManager.AddFixed(this);
            View = view;

            hitPoints.OnDeath += () => Died?.Invoke(this);
            _attackAgent.FirePerformed += (BulletSystemArgs args) => FirePerformed?.Invoke(args);
        }

        public void FixedTick()
        {
            _attackAgent.Update();
            _moveAgent.Update();
        }

        public void Attack(IUnitView target) => _attackAgent.SetTarget(target);

        public void Move(Vector3 to) => _moveAgent.SetDestination(to);
        
        public void Dispose() => _tickableManager.RemoveFixed(this);
    }
}