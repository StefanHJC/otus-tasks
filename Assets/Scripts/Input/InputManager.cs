using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private InputSchema _schema;

        private AttackInputListener _attackListener;
        private MoveInputListener _moveListener;

        public float HorizontalDirection => _moveListener.HorizontalDirection;

        public event Action AttackActionPerformed;

        private void Start()
        {
            _attackListener = new AttackInputListener(attackKey: _schema.AttackKey);
            _moveListener = new MoveInputListener(moveLeftKey: _schema.MoveLeftKey, moveRightKey: _schema.MoveRightKey);
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