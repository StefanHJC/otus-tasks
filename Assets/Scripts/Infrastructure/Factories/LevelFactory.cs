using System.Linq;
using Zenject;

namespace ShootEmUp
{
    public class LevelFactory
    {
        private readonly LevelStaticData _data;
        private readonly IAssetProvider _assets;

        [Inject]
        public LevelFactory(IDatabaseService data, IAssetProvider assets)
        {
            _data = data.Get<LevelStaticData>().FirstOrDefault();
            _assets = assets;
        }

        public Level InstantiateLevel(int levelIndex) => _assets.Instantiate(_data.PrefabData.First(level => level.Index == levelIndex).LevelData);
    }
}