using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class MoveInputListener : ITickable, IMoveInput
    {
        private readonly KeyCode _moveLeftKey;
        private readonly KeyCode _moveRightKey;

        public float HorizontalDirection { get; private set; }

        public MoveInputListener(KeyCode moveLeftKey, KeyCode moveRightKey)
        {
            _moveLeftKey = moveLeftKey;
            _moveRightKey = moveRightKey;
        }

        public void Tick()
        {
            if (Input.GetKey(_moveLeftKey))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(_moveRightKey))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
    }
}