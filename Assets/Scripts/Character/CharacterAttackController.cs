using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterAttackController : IService, IDisposable
    {
        private readonly AttackInputListener _attackInput;
        private readonly BulletSystem _bulletSystem;

        public UnitView View { get; private set; }

        public CharacterAttackController(AttackInputListener attackInput, UnitView view, BulletSystem bulletSystem)
        {
            _attackInput = attackInput;
            _bulletSystem = bulletSystem;
            View = view;

            _attackInput.AttackActionPerformed += OnFlyBullet;
        }

        public void Dispose() => _attackInput.AttackActionPerformed -= OnFlyBullet;

        private void OnFlyBullet() => _bulletSystem.FlyBulletByArgs(View.Weapon.GetBulletArgs(Vector2.zero));
    }
}