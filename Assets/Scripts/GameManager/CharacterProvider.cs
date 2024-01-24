using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CharacterProvider
    {
        private readonly IAssetProvider _assets;
        private UnitView _characterView;

        public IUnitView CharacterView => _characterView;

        [Inject]
        public CharacterProvider(IAssetProvider assets)
        {
            _assets = assets;
        }

        private IUnitView InstantiateCharacter(Transform at, UnitView prefab)
        {
            UnitView unitViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;
            _characterView = unitViewInstance;

            return unitViewInstance;
        }
    }
}