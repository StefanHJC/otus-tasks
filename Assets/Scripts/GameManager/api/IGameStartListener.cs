using System;

namespace ShootEmUp
{
    public interface IGameStartListener
    {
        event Action OnGameStarted;
    }
}