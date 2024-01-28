using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public class SceneLoader : ISceneLoader
    {
        public async Task LoadAsync(int sceneIndex, Action onLoaded)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!loadOperation.isDone) 
                await Task.Yield();

            onLoaded.Invoke();
        }
    }
}