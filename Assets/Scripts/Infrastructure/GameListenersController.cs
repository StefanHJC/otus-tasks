using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameListenersController : MonoBehaviour, IService
    {
        private Dictionary<Type, List<IGameListener>> _gameListeners = new Dictionary<Type, List<IGameListener>>();
        private List<IUpdateListener> _updateListeners = new List<IUpdateListener>();
        private List<IFixedUpdateListener> _fixedUpdateListeners = new List<IFixedUpdateListener>();

        public void Add(IGameListener listener)
        {
            foreach (Type nestedType in listener.
                         GetType().
                         GetInterfaces().
                         Where(type => typeof(IGameListener).IsAssignableFrom(type)))
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

        private void Awake() => InvokeListeners<IAwakeListener>();

        private void Start() => InvokeListeners<IStartListener>();

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++) 
                _updateListeners[i].OnUpdate();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate();
        }

        private void InvokeListeners<T>() where T : class, IGameListener
        {
            Type type = typeof(T);

            if (!_gameListeners.ContainsKey(type) || _gameListeners[type] == null)
                return;

            for (int i = 0; i < _gameListeners[type].Count; i++)
            {
                var listener = _gameListeners[type][i];

                switch (type)
                {
                    case IAwakeListener:
                        var awakeListener = (IAwakeListener)listener;
                        awakeListener.OnAwake();

                        break;

                    case IStartListener:
                        var startListener = (IStartListener)listener;
                        startListener.OnStart();

                        break;

                    case IGamePauseListener:
                        var pauseListener = (IGamePauseListener)listener;
                        pauseListener.OnPause();

                        break;

                    case IGameResumeListener:
                        var resumeListener = (IGameResumeListener)listener;
                        resumeListener.OnResume();

                        break;
                }
            }
        }
    }
}