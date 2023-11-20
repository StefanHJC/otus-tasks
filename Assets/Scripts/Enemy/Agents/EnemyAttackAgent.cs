using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(BulletSystem.Args bulletArgs);

        public event FireHandler OnFire;

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private MovementObserver movementObserver;
        [SerializeField] private float countdown;

        private HitPointsComponent target;
        private float currentTime;

        private bool CanFire => currentTime <= 0 && movementObserver.IsMoving == false;

        public void SetTarget(HitPointsComponent target) => this.target = target;

        public void Reset() => this.currentTime = this.countdown;

        private void FixedUpdate()
        {
            this.currentTime -= Time.fixedDeltaTime;

            if (CanShotAndTargetAlive() == false)
                return;

            this.Fire();
        }

        private void Fire()
        {
            this.OnFire?.Invoke(weaponComponent.GetBulletArgs(target.transform.position));
            Reset();
        }

        private bool CanShotAndTargetAlive() => CanFire && this.target.IsHitPointsExists();
    }
}