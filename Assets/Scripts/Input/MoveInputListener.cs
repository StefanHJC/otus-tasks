using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class MoveInputListener : ITickable, IMoveInput
    {
        private readonly KeyCode _moveLeftKey;
        private readonly KeyCode _moveRightKey;

        public float HorizontalDirection { get; private set; }

        [Inject]
        public MoveInputListener(IDatabaseService data)
        {
            _moveLeftKey = data.Get<GameStaticData>().FirstOrDefault().InputSchema.MoveLeftKey; 
            _moveRightKey = data.Get<GameStaticData>().FirstOrDefault().InputSchema.MoveRightKey;
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