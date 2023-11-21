using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private MovementObserver _movementObserver;
        [SerializeField] private float _countdown;

        private HitPointsComponent _target;
        private float _currentTime;

        private bool CanFire => _currentTime <= 0 && _movementObserver.IsMoving == false;

        public event Action<BulletSystem.Args> FirePerformed;

        public void Reset() => _currentTime = _countdown;
        
        public void SetTarget(HitPointsComponent target) => _target = target;

        private void FixedUpdate()
        {
            _currentTime -= Time.fixedDeltaTime;

            if (CanShotAndTargetAlive() == false)
                return;

            Fire();
        }

        private void Fire()
        {
            FirePerformed?.Invoke(_weaponComponent.GetBulletArgs(_target.transform.position));
            Reset();
        }

        private bool CanShotAndTargetAlive() => CanFire && _target.IsHitPointsExists();
    }
}