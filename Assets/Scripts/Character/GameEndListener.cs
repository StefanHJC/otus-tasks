using UnityEngine;

namespace ShootEmUp
{
    public class GameEndListener : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent playerHealth;
        [SerializeField] private GameManager gameManager;

        private void Start() => playerHealth.hpEmpty += OnPlayerDeath;

        private void OnDisable() => playerHealth.hpEmpty -= OnPlayerDeath;

        private void OnPlayerDeath(GameObject _) => gameManager.FinishGame();
    }
}