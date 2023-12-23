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

        public CharacterView StartGame(Transform characterPosition, CharacterView characterView)
        {
            //TODO start pre game counting

            return InstantiateCharacterView(at: characterPosition, prefab: characterView);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private CharacterView InstantiateCharacterView(Transform at, CharacterView prefab)
        {
            CharacterView characterViewInstance = _assets.Instantiate(prefab);
            prefab.transform.position = at.position;
            prefab.transform.rotation = at.rotation;

            return characterViewInstance;
        }
    }

    public struct GameSessionData
    {
        public CharacterView CharacterView;
    }
}