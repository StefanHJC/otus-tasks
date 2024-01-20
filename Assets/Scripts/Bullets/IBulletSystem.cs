namespace ShootEmUp
{
    public interface IBulletSystem
    {
        void OnPause();
        void OnResume();
        void OnGameEnd();
        void FlyBulletByArgs(BulletSystem.Args args);
    }
}