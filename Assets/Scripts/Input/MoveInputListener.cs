using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveInputListener : IService, IUpdateListener
    {
        private readonly KeyCode _moveLeftKey;
        private readonly KeyCode _moveRightKey;

        public float HorizontalDirection { get; private set; }

        public MoveInputListener(KeyCode moveLeftKey, KeyCode moveRightKey)
        {
            _moveLeftKey = moveLeftKey;
            _moveRightKey = moveRightKey;
        }

        public void OnUpdate()
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