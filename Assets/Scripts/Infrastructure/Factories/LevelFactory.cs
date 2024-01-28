using System.Linq;
using Zenject;

namespace ShootEmUp
{
    public class LevelFactory
    {
        private readonly IDatabaseService _data;
        private readonly IAssetProvider _assets;
        private readonly LevelProvider _provider;

        [Inject]
        public LevelFactory(IDatabaseService data, IAssetProvider assets, LevelProvider provider)
        {
            _data = data;
            _assets = assets;
            _provider = provider;
        }

        public Level InstantiateLevel(int levelIndex)
        {
            Level levelInstance = _assets.Instantiate(_data.
                                                    Get<LevelStaticData>().
                                                    FirstOrDefault().
                                                    PrefabData.
                                                    First(level => level.Index == levelIndex).Prefab);

            _provider.Level ??= levelInstance;

            return levelInstance;
        }
    }
}