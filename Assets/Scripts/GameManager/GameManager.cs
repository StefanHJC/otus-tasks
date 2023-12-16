using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }

    public sealed class GameListenersController : MonoBehaviour
    {
        private Dictionary<Type, List<IGameListener>> _gameListeners = new Dictionary<Type, List<IGameListener>>();
        private List<IUpdateListener> _updateListeners;
        private List<IFixedUpdateListener> _fixedUpdateListeners;

        public void Add(IGameListener listener)
        {
            foreach (Type nestedType in listener.
                                         GetType().
                                         GetNestedTypes().
                                         Where(type => type.IsSubclassOf(typeof(IGameListener))))
            {
                if (nestedType is IUpdateListener updateListener)
                {
                    _updateListeners.Add(updateListener);
                    
                    continue;
                }
                if (nestedType is IFixedUpdateListener fixedUpdateListener)
                {
                    _fixedUpdateListeners.Add(fixedUpdateListener);

                    continue;
                }
                if (_gameListeners.ContainsKey(nestedType) == false)
                {
                    _gameListeners.Add(nestedType, new List<IGameListener>() {listener});
                }
                else
                {
                    _gameListeners[nestedType].Add(listener);
                }
            }
        }

        public void Pause() => InvokeListeners<IGamePauseListener>();

        public void Resume() => InvokeListeners<IGameResumeListener>();

        private void Awake()
        {
            
            InvokeListeners<IAwakeListener>();
        }

        private void Start() => InvokeListeners<IStartListener>();

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++) 
                _updateListeners[i].Invoke();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].Invoke();
        }

        private void InvokeListeners<T>() where T : class, IGameListener
        {
            Type type = typeof(T);

            if (_gameListeners[type] == null)
                return;

            for (int i = 0; i < _gameListeners[type].Count; i++)
            {
                var listener = _gameListeners[type][i] as T;
                listener.Invoke();
            }
        }
    }

    public sealed class GameListenerProvider : MonoBehaviour
    {
        public Dictionary<Type, List<IGameListener>> GetListeners()
        {
            foreach (Transform child in gameObject.scene.GetRootGameObjects().Select(go => go.transform))
            {

            }

            return new Dictionary<Type, List<IGameListener>>();
        }
    }

    public interface IGameListener
    {
        void Invoke();
    }

    public interface IUpdateListener : IGameListener
    {
        new void Invoke();
    }

    public interface IFixedUpdateListener : IGameListener
    {
        new void Invoke();
    }

    public interface IStartListener : IGameListener
    {
        new void Invoke();
    }

    public interface IAwakeListener : IGameListener
    {
        new void Invoke();
    }
    
    public interface IEnableListener : IGameListener
    {
        new void Invoke();
    }

    public interface IDisableListener : IGameListener
    {
        new void Invoke();
    }

    public interface IGamePauseListener : IGameListener
    {
        new void Invoke();
    }

    public interface IGameResumeListener : IGameListener
    {
        new void Invoke();
    }
}