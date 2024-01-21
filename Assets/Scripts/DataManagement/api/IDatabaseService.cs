using UnityEngine;

namespace ShootEmUp
{
    public interface IDatabaseService
    {
        T Get<T>() where T : Object;
        void Load<T>(string path) where T : Object;
    }
}