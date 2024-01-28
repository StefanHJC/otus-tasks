using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindPlayerDeathListener();
            BindGameManager();

            BindEnemyFactory();
            BindEnemyPool();
            BindEnemyManager();

            BindBulletBuilder();
            BindBulletPool();
            BindBulletSystem();

            BindCharacter();
            BindCharacterProvider();

            BindUIProvider();
            BindUIFactory();
            BindUIMediator();
            BindButtonListener();
        }

        private void BindUIProvider()
        {
            Container.
                Bind<UIProvider>().
                AsSingle();
        }

        private void BindCharacterProvider()
        {
            Container.
                Bind<CharacterProvider>().
                AsSingle();
        }

        private void BindButtonListener()
        {
            Container.
                Bind<ButtonClickListener>().
                AsSingle();
        }

        private void BindUIFactory()
        {
            Container.
                Bind<UIFactory>().
                AsSingle();
        }

        private void BindUIMediator()
        {
            Container.
                Bind<UIMediator>().
                AsSingle();
        }

        private void BindCharacter()
        {
            Container.
                Bind<CharacterMoveController>().
                AsSingle();

            Container.
                Bind<CharacterAttackController>().
                AsSingle();
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
                BindInterfacesTo<BulletSystem>().
                AsSingle();
        }

        private void BindInput()
        {
            Container.
                BindInterfacesAndSelfTo<AttackInputListener>().
                AsSingle();
            
            Container.
                BindInterfacesAndSelfTo<MoveInputListener>().
                AsSingle();
        }

        private void BindBulletPool()
        {
            Container.
                Bind<IBulletPool>().
                To<BulletPool>().
                AsSingle();
        }

        private void BindBulletBuilder()
        {
            Container.
                Bind<IBulletBuilder>().
                To<BulletBuilder>().
                AsSingle();
        }

        private void BindEnemyManager()
        {
            Container.
                Bind<IEnemyManager>().
                To<EnemyManager>().
                AsSingle();
        }

        private void BindEnemyPool()
        {
            Container.
                Bind<IEnemyPool>().
                To<EnemyPool>().
                AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.
                Bind<IEnemyFactory>().
                To<EnemyFactory>().
                AsSingle();
        }

        private void BindPlayerDeathListener()
        {
            Container.
                Bind<PlayerDeathListener>().
                AsSingle();
        }
    }
}