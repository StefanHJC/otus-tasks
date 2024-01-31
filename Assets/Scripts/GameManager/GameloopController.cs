using System;

namespace ShootEmUp
{
    public sealed class GameloopController : IDisposable
    {
        private readonly CharacterFactory _characterFactory;
        private readonly UIMediator _uiMediator;
        private readonly Game _game;
        private readonly IGameInterruptionController _gameInterruptionController;
        private readonly IGameStartListener _gameStartListener;
        private readonly IGameEndListener _gameEndListener;
        private readonly IEnemyManager _enemyManager;

        public GameloopController(IGameStartListener gameStartListener, IGameEndListener gameEndListener, CharacterFactory characterFactory, 
                                IEnemyManager enemyManager, UIMediator uiMediator, IGameInterruptionController gameInterruptionController,
                                Game game)
        {
            _gameStartListener = gameStartListener;
            _gameEndListener = gameEndListener;
            _characterFactory = characterFactory;
            _enemyManager = enemyManager;
            _uiMediator = uiMediator;
            _gameInterruptionController = gameInterruptionController;
            _game = game;

            _gameStartListener.OnGameStarted += OnGameStart;
            _gameEndListener.OnGameEnded += OnGameEnd;
        }

        public void Dispose()
        {
            _gameStartListener.OnGameStarted -= OnGameStart;
            _gameEndListener.OnGameEnded -= OnGameEnd;
        }

        private void OnGameStart()
        {
            _game.State = GameState.Playing;

            _characterFactory.InstantiateCharacter(at: default);
            _enemyManager.Initialize();
            _uiMediator.ShowPauseButton();
        }

        private void OnGameEnd()
        {
            _uiMediator.ShowScreenText("Game over!!!");
            _uiMediator.HidePauseButton();
            _uiMediator.HideResumeButton();

            _gameInterruptionController.Pause();
        }
    }
}