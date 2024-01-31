using System.Linq;
using System.Numerics;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterFactory
    {
        private readonly CharacterProvider _provider;
        private readonly UnitStaticData.UnitPrefabData _data;
        private readonly DiContainer _di;
        private readonly IAssetProvider _assetProvider;

        [Inject]
        public CharacterFactory(CharacterProvider provider, DiContainer container, IDatabaseService data, IAssetProvider assetProvider)
        {
            _provider = provider;
            _di = container;
            _assetProvider = assetProvider;
            _data = data.
                Get<UnitStaticData>().
                FirstOrDefault().
                PrefabData.
                First(prefab => prefab.TypeId == UnitTypeId.Player);
        }

        public CharacterController InstantiateCharacter(Vector3 at)
        {
            UnitView viewInstance = _assetProvider.Instantiate(_data.Prefab).GetComponent<UnitView>();
            CharacterController characterInstance = _di.Instantiate<CharacterController>(new[] { viewInstance });

            _provider.Character ??= characterInstance;
            _provider.CharacterHealth ??= viewInstance.GetComponent<HitPointsComponent>();

            return characterInstance;
        }
    }
}