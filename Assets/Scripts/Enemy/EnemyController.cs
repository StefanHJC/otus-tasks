using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : IService, IFixedUpdateListener, IGamePauseListener, IGameResumeListener
    {
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly MovementObserver _movementObserver;
        private bool _isEnabled;

        public UnitView View { get; private set; }

        public event Action<BulletSystem.Args> FirePerformed;
        public event Action<EnemyController> Died;

        public EnemyController(UnitView view, HitPointsComponent hitPoints)
        {
            _moveAgent = new EnemyMoveAgent(view);
            _movementObserver = new MovementObserver(_moveAgent);
            _attackAgent = new EnemyAttackAgent(view, _movementObserver);
            View = view;
            _isEnabled = true;

            hitPoints.DeathHappened += () => Died?.Invoke(this);
            _attackAgent.FirePerformed += (BulletSystem.Args args) => FirePerformed?.Invoke(args);
        }

        public void OnPause() => _isEnabled = false;

        public void OnResume() => _isEnabled = true;

        public void OnFixedUpdate()
        {
            if (!_isEnabled)
                return;

            _attackAgent.Update();
            _moveAgent.Update();
        }

        public void Attack(Transform target) => _attackAgent.SetTarget(target.GetComponent<HitPointsComponent>());

        public void Move(Vector3 to) => _moveAgent.SetDestination(to);
    }
}