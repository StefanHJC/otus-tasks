using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : IService
    {
        private readonly CharacterAttackController _attackController;
        private readonly CharacterMoveController _moveController;

        public UnitView View { get; private set; }

        public CharacterController(CharacterAttackController attackController, CharacterMoveController moveController, UnitView view)
        {
            View = view;
            _attackController = attackController;
            _moveController = moveController;
        }
    }
}