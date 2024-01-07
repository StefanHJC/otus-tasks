
using Zenject;

namespace ShootEmUp
{
    public class EnemyFactory 
    {
        private readonly AssetProvider _assetProvider;
        private readonly UnitView _prefab;
        private readonly DiContainer _diContainer;

        [Inject]
        public EnemyFactory(UnitView prefab, AssetProvider assetProvider, DiContainer diContainer)
        {
            _prefab = prefab;
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public EnemyController GetEnemy()
        {
            UnitView viewInstance = _assetProvider.Instantiate(_prefab);

            return _diContainer.Instantiate<EnemyController>(new object[]
                { viewInstance, viewInstance.GetComponent<HitPointsComponent>() });
        }
    }
}