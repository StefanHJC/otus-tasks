namespace ShootEmUp
{
    public class EnemyFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly UnitView _prefab;

        public EnemyFactory(UnitView prefab, AssetProvider assetProvider)
        {
            _prefab = prefab;
            _assetProvider = assetProvider;
        }

        public EnemyController GetEnemy()
        {
            UnitView viewInstance = _assetProvider.Instantiate(_prefab);

            return new EnemyController(viewInstance);
        }
    }
}