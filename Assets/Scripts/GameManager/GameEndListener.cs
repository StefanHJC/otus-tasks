
namespace ShootEmUp
{
    public sealed class PlayerDeathListener
    {
        private HitPointsComponent _playerHealth;
        private GameManager _gameManager;

        public PlayerDeathListener(HitPointsComponent playerHealth, GameManager gameManager)
        {
            _playerHealth = playerHealth;
            _gameManager = gameManager;

            _playerHealth.DeathHappened += OnPlayerDeath;
        }

        private void OnPlayerDeath() => _gameManager.FinishGame();
    }
}