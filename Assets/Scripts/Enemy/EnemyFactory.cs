
namespace ShootEmUp
{
    public class EnemyFactory : IService
    {
        private readonly AssetProvider _assetProvider;
        private readonly UnitView _prefab;
        private readonly GameListenersController _gameListenersController;

        public EnemyFactory(UnitView prefab, GameListenersController gameListenersController, AssetProvider assetProvider)
        {
            _prefab = prefab;
            _assetProvider = assetProvider;
            _gameListenersController = gameListenersController;
        }

        public EnemyController GetEnemy()
        {
            UnitView viewInstance = _assetProvider.Instantiate(_prefab);

            var enemy = new EnemyController(viewInstance, viewInstance.GetComponent<HitPointsComponent>());
            _gameListenersController.Add(enemy.AttackAgent);
            _gameListenersController.Add(enemy.MoveAgent);

            return enemy;
        }
    }
}