using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        private static GameListenersController _gameListenerController;

        public static void Init(GameListenersController gameListenerController)
        {
            _gameListenerController = gameListenerController;
        }

        public static void Bind<T>(T service) where T : class, IService
        {
            _services[service.GetType()] = service;

            if (service is IGameListener gameListener)
                _gameListenerController.Add(gameListener);
        }

        public static T Get<T>() where T : class, IService => _services[typeof(T)] as T;
    }

    public interface IService
    {
    }
}