using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ShootEmUp
{
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