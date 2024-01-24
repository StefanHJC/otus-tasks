using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterAttackController
    {
        private readonly IAttackInput _attackInput;
        private readonly IBulletSystem _bulletSystem;

        public IUnitView View { get; private set; }

        [Inject]
        public CharacterAttackController(IAttackInput attackInput, IBulletSystem bulletSystem, IUnitView view)
        {
            _attackInput = attackInput;
            _bulletSystem = bulletSystem;
            View = view;

            _attackInput.AttackActionPerformed += OnFlyBullet;
        }

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(View.Weapon.GetBulletArgs(Vector2.zero));
    }
}