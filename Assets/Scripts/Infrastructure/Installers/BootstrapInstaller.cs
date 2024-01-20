using Zenject;

namespace ShootEmUp
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().
                To<AssetProvider>().
                FromNew().
                AsSingle().
                Lazy();
        }
    }
}