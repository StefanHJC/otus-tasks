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
            foreach (Type inheritedType in listener.
                         GetType().
                         GetInterfaces().
                         Where(type => typeof(IGameListener).IsAssignableFrom(type) && type is not IGameListener))
            {
                if (inheritedType == typeof(IUpdateListener))
                {
                    _updateListeners.Add(listener as IUpdateListener);
                    
                    continue;
                }
                if (inheritedType == typeof(IFixedUpdateListener))
                {
                    _fixedUpdateListeners.Add(listener as IFixedUpdateListener);

                    continue;
                }
                if (_gameListeners.ContainsKey(inheritedType) == false)
                {
                    _gameListeners.Add(inheritedType, new List<IGameListener>() {listener});
                }
                else
                {
                    _gameListeners[inheritedType].Add(listener);
                }
            }
        }

        public bool TryRemove(IGameListener listener) =>
            _gameListeners[listener.GetType()].Remove(listener) ||
            _updateListeners.Remove(listener as IUpdateListener) ||
            _fixedUpdateListeners.Remove(listener as IFixedUpdateListener);

        public void StartGame() => InvokeListeners<IGameStartListener>();

        public void EndGame() => InvokeListeners<IGameEndListener>();

        public void Pause() => InvokeListeners<IGamePauseListener>();

        public void Resume() => InvokeListeners<IGameResumeListener>();

        private void Awake() => InvokeListeners<IAwakeListener>();

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

        private void OnDestroy() => EndGame();

        private void InvokeListeners<T>() where T : class, IGameListener
        {
            Type type = typeof(T);

            if (!_gameListeners.ContainsKey(type) || _gameListeners[type] == null)
                return;

            for (int i = 0; i < _gameListeners[type].Count; i++)
            {
                var listener = _gameListeners[type][i];

                if (type == typeof(IAwakeListener))
                {
                    var awakeListener = (IAwakeListener)listener;
                    awakeListener.OnAwake();

                    break;
                }
                else if (type == typeof(IGameStartListener))
                {
                    var startListener = (IGameStartListener)listener;
                    startListener.OnGameStart();

                    break;
                }
                else if (type == typeof(IGameEndListener))
                {
                    var endListener = (IGameEndListener)listener;
                    endListener.OnGameEnd();

                    break;
                }
                else if (type == typeof(IGamePauseListener))
                {
                    var pauseListener = (IGamePauseListener)listener;
                    pauseListener.OnPause();

                    break;
                }
                else if (type == typeof(IGameResumeListener))
                {
                    var resumeListener = (IGameResumeListener)listener;
                    resumeListener.OnResume();

                    break;
                }
            }
        }
    }
}