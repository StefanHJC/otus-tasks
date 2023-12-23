using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    {
        private readonly IWeaponComponent _weaponComponent;
        private readonly MovementObserver _movementObserver;
        private HitPointsComponent _target;
        private float _currentTime;

        private bool CanFire => _currentTime <= 0 && _movementObserver.IsMoving == false;

        public float Countdown { get; set; } = 1;

        public event Action<BulletSystem.Args> FirePerformed;

        public EnemyAttackAgent(IUnitView view, MovementObserver movementObserver)
        {
             _weaponComponent = view.Weapon;
             _movementObserver = movementObserver;
        }

        public void Update()
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
            FirePerformed?.Invoke(_weaponComponent.GetBulletArgs(_target.transform.position));
            Reset();
        }

        private bool CanShotAndTargetAlive() => CanFire && _target.IsHitPointsExists();
    }
}