using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ShootEmUp
{
    public class GameEntry : MonoBehaviour
    {
        private IDatabaseService _data;
        private UIFactory _uiFactory;
        private LevelFactory _levelfactory;

        [Inject]
        public void Construct(IDatabaseService data, UIFactory uiFactory, LevelFactory levelFactory)
        {
            _data = data;
            _uiFactory = uiFactory;
            _levelfactory = levelFactory;
        }

        private void Awake()
        {
            LoadStaticData();
            SceneManager.LoadSceneAsync(1);
        }

        private void LoadStaticData()
        {
            _data.Load<BulletStaticData>(AssetPath.StaticData);
            _data.Load<GameStaticData>(AssetPath.StaticData);
            _data.Load<UIStaticData>(AssetPath.StaticData);
            _data.Load<UnitStaticData>(AssetPath.StaticData);
        }

        private void OnLevelLoaded()
        {
            _levelfactory.InstantiateLevel(0);
            _uiFactory.InstantiateHUD();
        }
    }
}