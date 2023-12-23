using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    {
        private WeaponComponent _weaponComponent;
        private MovementObserver _movementObserver;
        private float _countdown = 1;

        private HitPointsComponent _target;
        private float _currentTime;

        private bool CanFire => _currentTime <= 0 && _movementObserver.IsMoving == false;

        public event Action<BulletSystem.Args> FirePerformed;

        public EnemyAttackAgent()
        {

        }

        public void Update()
        {
            _currentTime -= Time.fixedDeltaTime;

            if (CanShotAndTargetAlive() == false)
                return;

            Fire();
        }

        public void Reset() => _currentTime = _countdown;

        public void SetTarget(HitPointsComponent target) => _target = target;

        private void Fire()
        {
            FirePerformed?.Invoke(_weaponComponent.GetBulletArgs(_target.transform.position));
            Reset();
        }

        private bool CanShotAndTargetAlive() => CanFire && _target.IsHitPointsExists();
    }

    public class EnemyController : IService, IFixedUpdateListener
    {
        private readonly EnemyAttackAgent _attackAgent;
        private readonly EnemyMoveAgent _moveAgent;

        public EnemyController()
        {
            _attackAgent = new EnemyAttackAgent();
            _moveAgent = new EnemyMoveAgent();
        }


        public void OnFixedUpdate()
        {
            _attackAgent.Update();
            //_moveAgent.Update();
        }

        public void Attack(Transform target) => _attackAgent.SetTarget(target.GetComponent<HitPointsComponent>());
    }
}