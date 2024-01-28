using System;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public interface ISceneLoader
    {
        Task LoadAsync(int sceneIndex, Action onLoaded);
    }
}