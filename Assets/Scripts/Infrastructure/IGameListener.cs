namespace ShootEmUp
{
    public interface IGameListener
    {
    }

    public interface IUpdateListener : IGameListener
    {
        void OnUpdate();
    }

    public interface IFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate();
    }

    public interface IAwakeListener : IGameListener
    {
        void OnAwake();
    }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }


    public interface IGameEndListener : IGameListener
    {
        void OnGameEnd();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }
}