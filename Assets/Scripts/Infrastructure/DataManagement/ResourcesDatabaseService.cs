using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public class ResourcesDatabaseService : IDatabaseService
    {
        private readonly Dictionary<Type, List<Object>> _loadedData;

        public ResourcesDatabaseService()
        {
            _loadedData = new Dictionary<Type, List<Object>>();
        }

        public IEnumerable<T> Get<T>() where T : Object
        {
            if (_loadedData == null || !_loadedData.ContainsKey(typeof(T)))
                throw new ArgumentException($"Data of type {typeof(T)} is not loaded");

            return _loadedData[typeof(T)].Select(data => data as T);
        }

        public void Load<T>(string path) where T : Object
        {
            foreach (var loaded in Resources.LoadAll<T>(path))
            {
                if (!_loadedData.ContainsKey(loaded.GetType()))
                {
                    _loadedData.Add(loaded.GetType(), new List<Object>() {loaded});
                }
                else
                {
                    _loadedData[loaded.GetType()].Add(loaded);
                }
            }
        }
    }
}