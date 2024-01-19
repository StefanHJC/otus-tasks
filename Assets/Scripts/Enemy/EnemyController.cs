using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : IService, IDisposable
    {
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly MovementObserver _movementObserver;
        private readonly HitPointsComponent _hitPointsComponent;

        public EnemyAttackAgent AttackAgent => _attackAgent;
        public EnemyMoveAgent MoveAgent => _moveAgent;
        public UnitView View { get; private set; }

        public event Action<BulletSystem.Args> FirePerformed;
        public event Action<EnemyController> Died;

        public EnemyController(UnitView view, HitPointsComponent hitPoints)
        {
            _moveAgent = new EnemyMoveAgent(view);
            _movementObserver = new MovementObserver(_moveAgent);
            _attackAgent = new EnemyAttackAgent(view, _movementObserver);
            _hitPointsComponent = hitPoints;
            View = view;

            _hitPointsComponent.DeathHappened += InvokeDeathEvent;
            _attackAgent.FirePerformed += InvokeFireEvent;
        }

        public void Dispose()
        {
            _hitPointsComponent.DeathHappened -= InvokeDeathEvent;
            _attackAgent.FirePerformed -= InvokeFireEvent;
        }

        public void Attack(Transform target) => _attackAgent.SetTarget(target.GetComponent<HitPointsComponent>());

        public void Move(Vector3 to) => _moveAgent.SetDestination(to);

        private void InvokeDeathEvent() => Died?.Invoke(this);

        private void InvokeFireEvent(BulletSystem.Args args) => FirePerformed?.Invoke(args);
    }
}