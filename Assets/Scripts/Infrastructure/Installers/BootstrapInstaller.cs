using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameEntry _gameEntryPoint;

        public override void InstallBindings()
        {
            BindAssetProvider();
            BindGameInterruption();
            BindDatabaseService();
            BindGameEntry();
            BindUIProvider();
            BindUIFactory();
            BindLevelFactory();
            BindLevelProvider();
            BindSceneLoader();
        }

        private void BindLevelProvider()
        {
            Container.
                Bind<LevelProvider>().
                AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.
                Bind<ISceneLoader>().
                To<SceneLoader>().
                AsSingle();
        }

        private void BindLevelFactory()
        {
            Container.
                Bind<LevelFactory>().
                AsSingle();
        }

        private void BindUIFactory()
        {
            Container.
                Bind<UIFactory>().
                AsSingle().Lazy();
        }

        private void BindUIProvider()
        {
            Container.
                Bind<UIProvider>().
                AsSingle().Lazy();
        }

        private void BindGameEntry()
        {
            Container.
                Bind<GameEntry>().
                FromComponentInNewPrefab(_gameEntryPoint).
                AsSingle().
                NonLazy();
        }

        private void BindDatabaseService()
        {
            Container.
                Bind<IDatabaseService>().
                To<ResourcesDatabaseService>().
                AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.
                Bind<IAssetProvider>().
                To<AssetProvider>().
                FromNew().
                AsSingle().
                Lazy();
        }

        private void BindGameInterruption()
        {
            Container.
                Bind<IGameInterruptionController>().
                To<ZenjectTickableInterruptionController>().
                AsSingle();
        }
    }
}