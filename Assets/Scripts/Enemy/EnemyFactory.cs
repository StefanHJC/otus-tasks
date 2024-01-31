
using System.Linq;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly UnitStaticData.UnitPrefabData _data;

        [Inject]
        public EnemyFactory(IAssetProvider assetProvider, IDatabaseService data, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;

            _data = data.
                        Get<UnitStaticData>().
                        FirstOrDefault().
                        PrefabData.
                        First(prefab => prefab.TypeId == UnitTypeId.Enemy);
        }

        public EnemyController GetEnemy()
        {
            UnitView viewInstance = _assetProvider.Instantiate(_data.Prefab.GetComponent<UnitView>());

            return _diContainer.Instantiate<EnemyController>(new object[]
                { viewInstance, viewInstance.GetComponent<HitPointsComponent>() });
        }
    }
}
