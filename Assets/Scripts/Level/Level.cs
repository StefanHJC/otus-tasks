using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform[] _enemyPositions;
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Transform _bulletParent;
        [SerializeField] private Transform _levelBackground;

        [Inject]
        public void Construct(Transform[] enemyPositions, Transform enemyParent, Transform bulletParent, Transform levelBackground)
        {
            _enemyPositions = enemyPositions;
            _enemyParent = enemyParent;
            _bulletParent = bulletParent;
            _levelBackground = levelBackground;
        }
    }
}