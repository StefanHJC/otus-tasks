using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameEntry : MonoBehaviour
    {
        private UIFactory _uiFactory;
        private LevelFactory _levelfactory;
        private IDatabaseService _data;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(IDatabaseService data, UIFactory uiFactory, LevelFactory levelFactory, ISceneLoader sceneLoader)
        {
            _data = data;
            _uiFactory = uiFactory;
            _levelfactory = levelFactory;
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            LoadStaticData();
            _sceneLoader.LoadAsync(sceneIndex: 1, onLoaded: OnSceneLoaded);
        }

        private void LoadStaticData()
        {
            _data.Load<BulletStaticData>(AssetPath.StaticData);
            _data.Load<GameStaticData>(AssetPath.StaticData);
            _data.Load<UIStaticData>(AssetPath.StaticData);
            _data.Load<UnitStaticData>(AssetPath.StaticData);
            _data.Load<LevelStaticData>(AssetPath.StaticData);
        }

        private void OnSceneLoaded()
        {
            _levelfactory.InstantiateLevel(0);
            _uiFactory.InstantiateHUD();
        }
    }
}