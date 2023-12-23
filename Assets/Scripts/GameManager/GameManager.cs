using UnityEditor;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : IService
    {
        private readonly GameListenersController _gameListenersController;
        private readonly AssetProvider _assets;

        public GameManager(GameListenersController gameListenersController, AssetProvider assets)
        {
            _gameListenersController = gameListenersController;
            _assets = assets;
        }

        public void PauseGame() => _gameListenersController.Pause();

        public void ResumeGame() => _gameListenersController.Resume();

        public UnitView StartGame(Transform characterPosition, UnitView unitView)
        {
            //TODO start pre game counting

            return InstantiateCharacterView(at: characterPosition, prefab: unitView);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private UnitView InstantiateCharacterView(Transform at, UnitView prefab)
        {
            UnitView unitViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;

            return unitViewInstance;
        }
    }

    public struct GameSessionData
    {
        public UnitView UnitView;
    }
}