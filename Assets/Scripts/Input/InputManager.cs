using System;

namespace ShootEmUp
{
    public sealed class InputManager : IService, IUpdateListener
    {
        private InputSchema _schema;
        private AttackInputListener _attackListener;
        private MoveInputListener _moveListener;

        public float HorizontalDirection => _moveListener.HorizontalDirection;

        public event Action AttackActionPerformed;

        public InputManager(InputSchema schema)
        {
            _schema = schema;

            Init();
        }

        public void OnUpdate()
        {
            ListenInput();
        }

        private void Init()
        {
            _attackListener = new AttackInputListener(attackKey: _schema.AttackKey);
            _moveListener = new MoveInputListener(moveLeftKey: _schema.MoveLeftKey, moveRightKey: _schema.MoveRightKey);
            _attackListener.AttackActionPerformed += () => AttackActionPerformed?.Invoke();
        }

        private void ListenInput()
        {
            _attackListener.Update();
            _moveListener.Update();
        }
    }
}