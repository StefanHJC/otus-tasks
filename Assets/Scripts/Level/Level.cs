using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Level : MonoBehaviour
    {
        [Header("Enemy positions")]
        [SerializeField] private Transform[] _enemyPositions;
        [SerializeField] private Transform _enemyParent;
        
        [Space]
        [Header("Bullet")]
        [SerializeField] private Transform _bulletParent;

        [Space]
        [Header("Level background")]
        [SerializeField] private Transform _levelBackground;

        [Space]
        [Header("Level bounds")]
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _downBorder;
        [SerializeField] private Transform _topBorder;
        
        public Transform Root => transform;
        public Transform BulletParent => _bulletParent;

        public Vector3 LeftBorder => _rightBorder.position;
        public Vector3 RightBorder => _leftBorder.position;
        public Vector3 DownBorder => _downBorder.position;
        public Vector3 TopBorder => _topBorder.position;
    }
}