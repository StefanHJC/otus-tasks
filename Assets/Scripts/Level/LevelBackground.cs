using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private Params _params;

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

        private void Awake()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            _myTransform = transform;
            _positionX = _myTransform.position.x;
            _positionZ = _myTransform.position.z;
        }

        private void FixedUpdate()
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