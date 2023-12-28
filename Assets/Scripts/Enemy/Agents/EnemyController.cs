using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : IService, IFixedUpdateListener
    {
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly MovementObserver _movementObserver;

        public UnitView View { get; private set; }

        public event Action<BulletSystem.Args> FirePerformed;

        public EnemyController(UnitView view)
        {
            _moveAgent = new EnemyMoveAgent(view);
            _movementObserver = new MovementObserver(_moveAgent);
            _attackAgent = new EnemyAttackAgent(view, _movementObserver);
            View = view;

            _attackAgent.FirePerformed += (BulletSystem.Args args) => FirePerformed?.Invoke(args);
        }

        public void OnFixedUpdate()
        {
            _attackAgent.Update();
            _moveAgent.Update();
        }

        public void Attack(Transform target) => _attackAgent.SetTarget(target.GetComponent<HitPointsComponent>());
    }
}