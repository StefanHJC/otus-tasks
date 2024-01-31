using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController
    {
        private readonly CharacterAttackController _attackController;
        private readonly CharacterMoveController _moveController;
        private readonly IUnitView _view;

        public IUnitView View => _view;

        [Inject]
        public CharacterController(CharacterAttackController attackController, CharacterMoveController moveController, IUnitView view)
        {
            _attackController = attackController;
            _moveController = moveController;
            _view = view;

            _moveController.View = _view;
            _attackController.View = _view;
        }
    }
}