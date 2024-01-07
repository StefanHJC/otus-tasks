using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindGameEndListener();
            BindGameManager();

            BindEnemyFactory();
            BindEnemyPool();
            BindEnemyFactory();
            BindEnemyManager();

            BindBulletBuilder();
            BindBulletPool();
            BindBulletSystem();

            BindHUD();
        }

        private void BindGameManager()
        {
            Container.
                Bind<GameManager>().
                FromNew().
                AsSingle().
                Lazy();
        }

        private void BindBulletSystem()
        {
            Container.
                BindInterfacesAndSelfTo<BulletSystem>().
                AsSingle();
        }

        private void BindInput()
        {
            Container.
                BindInterfacesAndSelfTo<InputManager>().
                AsSingle();
        }

        private void BindBulletPool()
        {
            Container.
                Bind<BulletPool>().
                AsSingle();
        }

        private void BindBulletBuilder()
        {
            Container.
                Bind<BulletBuilder>().
                AsSingle();
        }

        private void BindHUD()
        {
            Container.
                Bind<HUD>().
                AsSingle();
        }

        private void BindEnemyManager()
        {
            Container.
                Bind<EnemyManager>().
                AsSingle();
        }

        private void BindEnemyPool()
        {
            Container.
                Bind<EnemyPool>().
                AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.
                Bind<EnemyFactory>().
                AsSingle();
        }

        private void BindGameEndListener()
        {
            Container.
                Bind<GameEndListener>().
                AsSingle();
        }
    }
}