using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEndListener : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent playerHealth;
        [SerializeField] private GameManager gameManager;

        private void Start() => playerHealth.DeathHappened += OnPlayerDeath;

        private void OnDisable() => playerHealth.DeathHappened -= OnPlayerDeath;

        private void OnPlayerDeath(GameObject _) => gameManager.FinishGame();
    }
}