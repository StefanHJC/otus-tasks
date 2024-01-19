using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : IService, IFixedUpdateListener
    {
        private Transform _myTransform;
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float StartPositionY;
            [SerializeField] public float EndPositionY;
            [SerializeField] public float MovingSpeedY;
        }

        public LevelBackground(Params pParams, Transform backgroundParent)
        {
            _startPositionY = pParams.StartPositionY;
            _endPositionY = pParams.EndPositionY;
            _movingSpeedY = pParams.MovingSpeedY;
            _myTransform = backgroundParent;
            _positionX = _myTransform.position.x;
            _positionZ = _myTransform.position.z;
        }

        public void OnFixedUpdate()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ);
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ);
        }
    }
}