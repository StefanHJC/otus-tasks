using Zenject;

namespace ShootEmUp
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameManager();
            BindAssetProvider();
        }

        private void BindAssetProvider()
        {
            Container.
                Bind<AssetProvider>().
                FromNew().
                AsSingle().
                Lazy();
        }

        private void BindGameManager()
        {
            Container.
                Bind<GameManager>().
                FromNew().
                AsSingle().
                Lazy();
        }
    }
}