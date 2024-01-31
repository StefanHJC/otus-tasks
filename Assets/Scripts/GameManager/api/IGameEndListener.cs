using System;

namespace ShootEmUp
{
    public interface IGameEndListener
    {
        event Action OnGameEnded;
    }
}