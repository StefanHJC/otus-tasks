using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMoveController : IService, IFixedUpdateListener
    {
        private readonly MoveInputListener _moveInput;

        public UnitView View { get; private set; }

        public CharacterMoveController(MoveInputListener moveInput, UnitView view)
        {
            _moveInput = moveInput;
            View = view;
        }

        public void OnFixedUpdate()
        {
            View.Movement.Move(new Vector2(_moveInput.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}