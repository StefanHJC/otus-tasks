using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public sealed class GameListenerController : MonoBehaviour
    {
        private Dictionary<Type, List<IGameListener>> _gameListeners = new Dictionary<Type, List<IGameListener>>();
        private List<IUpdateListener> _updateListeners;
        private List<IFixedUpdateListener> _fixedUpdateListeners;

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

    public class GameListenerInstaller : MonoBehaviour
    {
        public Dictionary<Type, List<IGameListener>> GetListeners()
        {
            var listeners = GetComponentsInChildren<IGameListener>(includeInactive: true);

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

    public interface IGamePauseListener : IGameListener
    {
        new void Invoke();
    }

    public interface IGameResumeListener : IGameListener
    {
        new void Invoke();
    }
}