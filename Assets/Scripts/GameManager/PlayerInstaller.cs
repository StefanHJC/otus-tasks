using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerInstaller : IService
    {
        private readonly CharacterProvider _characterProvider;
        private readonly AssetProvider _assets;
        private readonly UnitView _prefab;
        private readonly Transform _position;

        public PlayerInstaller(CharacterProvider characterProvider, AssetProvider assets, UnitView prefab, Transform position)
        {
            _characterProvider = characterProvider;
            _assets = assets;
            _prefab = prefab;
            _position = position;
        }

        public void InstallGameSessionBindings(UnitView view)
        {
            ServiceLocator.Bind<CharacterController>(new CharacterController(
                ServiceLocator.Get<InputManager>(),
                ServiceLocator.Get<BulletSystem>(),
                view));
            _characterProvider.Character = ServiceLocator.Get<CharacterController>();
        }

        public UnitView InstantiateCharacterView()
        {
            UnitView unitViewInstance = _assets.Instantiate(_prefab);
            unitViewInstance.transform.position = _position.position;
            unitViewInstance.transform.rotation = _position.rotation;

            return unitViewInstance;
        }
    }
}