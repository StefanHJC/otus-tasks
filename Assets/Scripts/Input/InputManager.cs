using System;

namespace ShootEmUp
{
    public sealed class InputManager : IService,
        IUpdateListener, 
        IGamePauseListener, 
        IGameResumeListener, 
        IGameEndListener,
        IDisposable
    {
        private InputSchema _schema;
        private AttackInputListener _attackListener;
        private MoveInputListener _moveListener;
        private bool _isEnabled;

        public float HorizontalDirection => _moveListener.HorizontalDirection;

        public event Action AttackActionPerformed;

        public InputManager(InputSchema schema)
        {
            _schema = schema;

            Init();
        }

        public void OnUpdate()
        {
            if (_isEnabled)
                ListenInput();
        }

        public void OnPause() => _isEnabled = false;

        public void OnResume() => _isEnabled = true;

        public void OnGameEnd() => _isEnabled = true;

        public void Dispose() => _attackListener.AttackActionPerformed -= InvokeAttackEvent;

        private void Init()
        {
            _attackListener = new AttackInputListener(attackKey: _schema.AttackKey);
            _moveListener = new MoveInputListener(moveLeftKey: _schema.MoveLeftKey, moveRightKey: _schema.MoveRightKey);
            _attackListener.AttackActionPerformed += InvokeAttackEvent;
            _isEnabled = true;
        }

        private void ListenInput()
        {
            _attackListener.Update();
            _moveListener.Update();
        }

        private void InvokeAttackEvent() => AttackActionPerformed?.Invoke();
    }
}