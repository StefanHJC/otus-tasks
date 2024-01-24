using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterMoveController : IFixedTickable, IDisposable
    {
        private readonly IMoveInput _moveInput;
        private readonly TickableManager _tickableManager;

        public IUnitView View { get; private set; }

        [Inject]
        public CharacterMoveController(IMoveInput moveInput, IUnitView view, TickableManager tickableManager)
        {
            _moveInput = moveInput;
            View = view;
            _tickableManager = tickableManager;
            _tickableManager.AddFixed(this);
        }

        public void FixedTick()
        {
            View.Movement.Move(new Vector2(_moveInput.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        public void Dispose() => _tickableManager.RemoveFixed(this);
    }
}