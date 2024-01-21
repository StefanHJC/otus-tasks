namespace ShootEmUp
{
    public interface IEnemyManager
    {
        void Initialize();
        void OnGameEnd();
        void OnPause();
        void OnResume();
    }
}