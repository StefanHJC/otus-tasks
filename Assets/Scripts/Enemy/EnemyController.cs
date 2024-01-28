using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyController : IFixedTickable
    {
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly MovementObserver _movementObserver;

        public UnitView View { get; private set; }

        public event Action<BulletSystemArgs> FirePerformed;
        public event Action<EnemyController> Died;

        public EnemyController(UnitView view, HitPointsComponent hitPoints)
        {
            _moveAgent = new EnemyMoveAgent(view);
            _movementObserver = new MovementObserver(_moveAgent);
            _attackAgent = new EnemyAttackAgent(view, _movementObserver);
            View = view;

            hitPoints.OnDeath += () => Died?.Invoke(this);
            _attackAgent.FirePerformed += (BulletSystemArgs args) => FirePerformed?.Invoke(args);
        }

        public void FixedTick()
        {
            _attackAgent.Update();
            _moveAgent.Update();
        }

        public void Attack(Transform target) => _attackAgent.SetTarget(target.GetComponent<HitPointsComponent>());

        public void Move(Vector3 to) => _moveAgent.SetDestination(to);
    }
}