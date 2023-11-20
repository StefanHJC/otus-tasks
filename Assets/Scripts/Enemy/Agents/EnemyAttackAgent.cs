using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

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
            this.currentTime += this.countdown;
        }

        private void Fire() => 
            this.OnFire?.Invoke(this.gameObject, weaponComponent.Position, weaponComponent.GetDirectionToTarget(target.transform.position));

        private bool CanShotAndTargetAlive() => CanFire && this.target.IsHitPointsExists();
    }
}