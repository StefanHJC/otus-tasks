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
        private bool _isEnabled;

        public UnitView View { get; private set; }

        public event Action<BulletSystemArgs> FirePerformed;
        public event Action<EnemyController> Died;

        public EnemyController(UnitView view, HitPointsComponent hitPoints)
        {
            _moveAgent = new EnemyMoveAgent(view);
            _movementObserver = new MovementObserver(_moveAgent);
            _attackAgent = new EnemyAttackAgent(view, _movementObserver);
            View = view;
            _isEnabled = true;

            hitPoints.DeathHappened += () => Died?.Invoke(this);
            _attackAgent.FirePerformed += (BulletSystemArgs args) => FirePerformed?.Invoke(args);
        }

        public void FixedTick()
        {
            if (!_isEnabled)
                return;

            _attackAgent.Update();
            _moveAgent.Update();
        }

        public void OnPause() => _isEnabled = false;

        public void OnResume() => _isEnabled = true;

        public void Attack(Transform target) => _attackAgent.SetTarget(target.GetComponent<HitPointsComponent>());

        public void Move(Vector3 to) => _moveAgent.SetDestination(to);
    }
}