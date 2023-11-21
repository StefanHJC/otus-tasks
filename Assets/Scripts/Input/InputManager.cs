using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        private const KeyCode AttackKey = KeyCode.Space;
        private const KeyCode MoveLeftKey = KeyCode.LeftArrow;
        private const KeyCode MoveRightKey = KeyCode.RightArrow;

        private AttackInputListener _attackListener;
        private MoveInputListener _moveListener;

        public float HorizontalDirection => _moveListener.HorizontalDirection;

        public event Action AttackActionPerformed;

        private void Start()
        {
            _attackListener = new AttackInputListener(attackKey: AttackKey);
            _moveListener = new MoveInputListener(moveLeftKey: MoveLeftKey, moveRightKey: MoveRightKey);
            _attackListener.AttackActionPerformed = AttackActionPerformed;
        }

        private void Update()
        {
            ListenInput();
        }

        private void ListenInput()
        {
            _attackListener.Update();
            _moveListener.Update();
        }
    }
}