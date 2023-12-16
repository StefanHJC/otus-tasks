using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }

    public sealed class GameListenerProvider : MonoBehaviour
    {
        public Dictionary<Type, List<IGameListener>> GetListeners()
        {
            foreach (Transform child in gameObject.scene.GetRootGameObjects().Select(go => go.transform))
            {

            }

            return new Dictionary<Type, List<IGameListener>>();
        }
    }
}