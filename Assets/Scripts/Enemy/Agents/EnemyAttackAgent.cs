using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : IService, IFixedUpdateListener
    {
        private readonly MovementObserver _movementObserver;
        private readonly BulletSystem _bulletSystem;
        private readonly IWeaponComponent _weaponComponent;
        private HitPointsComponent _target;
        private float _currentTime;

        private bool CanFire => _currentTime <= 0 && _movementObserver.IsMoving == false;

        public float Countdown { get; set; } = 1;

        public EnemyAttackAgent(IUnitView view, MovementObserver movementObserver, BulletSystem bulletSystem)
        {
             _weaponComponent = view.Weapon;
             _movementObserver = movementObserver;
             _bulletSystem = bulletSystem;
        }

        public void OnFixedUpdate()
        {
            _currentTime -= Time.fixedDeltaTime;

            if (CanShotAndTargetAlive() == false)
                return;

            Fire();
        }

        public void Reset() => _currentTime = Countdown;

        public void SetTarget(HitPointsComponent target) => _target = target;

        private void Fire()
        {
            _bulletSystem.FlyBulletByArgs(_weaponComponent.GetBulletArgs(_target.transform.position));
            Reset();
        }

        private bool CanShotAndTargetAlive() => CanFire && (_target != null && _target.IsHitPointsExists());
    }
}