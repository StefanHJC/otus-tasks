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
            Container.Bind<AssetProvider>().FromNew().AsSingle().Lazy();
        }
    }
}