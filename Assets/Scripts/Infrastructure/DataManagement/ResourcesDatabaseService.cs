using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
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

        public T Get<T>() where T : Object
        {
            if (_loadedData == null || !_loadedData.ContainsKey(typeof(T)))
                throw new ArgumentException($"Data of type {typeof(T)} is not loaded");

            return _loadedData[typeof(T)] as T;
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

    public class GameEntry : MonoBehaviour
    {
        private IDatabaseService _data;

        [Inject]
        public void Construct(IDatabaseService data)
        {
            _data = data;
        }

        private void Awake()
        {
            LoadStaticData();
            SceneManager.LoadSceneAsync(1);
            
            DontDestroyOnLoad(this);
        }

        private void LoadStaticData()
        {
            _data.Load<BulletStaticData>(AssetPath.StaticData);
            _data.Load<GameStaticData>(AssetPath.StaticData);
            _data.Load<UIStaticData>(AssetPath.StaticData);
            _data.Load<UnitStaticData>(AssetPath.StaticData);
        }
    }
}