using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : ITickable
    {
        private InputSchema _schema;
        private AttackInput _attack;
        private MoveInputListener _moveListener;
        private bool _isEnabled;

        public float HorizontalDirection => _moveListener.HorizontalDirection;

        public event Action AttackActionPerformed;

        public InputManager(InputSchema schema)
        {
            _schema = schema;

            Init();
        }

        public void Tick()
        {
            if (_isEnabled)
                ListenInput();
        }

        public void OnPause() => _isEnabled = false;

        public void OnResume() => _isEnabled = true;

        public void OnGameEnd() => _isEnabled = true;

        private void Init()
        {
            _attack = new AttackInput(attackKey: _schema.AttackKey);
            _moveListener = new MoveInputListener(moveLeftKey: _schema.MoveLeftKey, moveRightKey: _schema.MoveRightKey);
            _attack.AttackActionPerformed += () => AttackActionPerformed?.Invoke();
            _isEnabled = true;
        }

        private void ListenInput()
        {
            _attack.Update();
            _moveListener.Update();
        }
    }
}