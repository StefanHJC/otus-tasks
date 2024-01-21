using Zenject;

namespace ShootEmUp
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindGameInterruption();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().
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