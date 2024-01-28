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
            BindGameLauncher();
            BindGameloopController();

            BindEnemyFactory();
            BindEnemyPool();
            BindEnemyManager();

            BindBulletBuilder();
            BindBulletPool();
            BindBulletSystem();

            BindCharacter();
            BindCharacterProvider();

            BindUIMediator();
            BindButtonListener();

            BindLevelBounds();
            //BindLevelBackground(level);
        }

        private void BindGameloopController()
        {
            Container.
                BindInterfacesAndSelfTo<GameloopController>().
                AsSingle();
        }

        private void BindGameLauncher()
        {
            Container.
                BindInterfacesAndSelfTo<GameLauncher>().
                AsSingle();
        }

        private void BindLevelBackground()
        {
            Container.
                Bind<LevelBackground>().
                AsSingle();
        }

        private void BindLevelBounds()
        {
            Container.
                Bind<LevelBounds>().
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
                BindInterfacesAndSelfTo<ButtonClickListener>().
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
                Bind<CharacterAttackController>().
                AsSingle();

            Container.
                BindInterfacesAndSelfTo<CharacterMoveController>().
                AsTransient();

            Container.
                BindInterfacesAndSelfTo<CharacterAttackController>().
                AsTransient();
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
                BindInterfacesAndSelfTo<BulletPool>().
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