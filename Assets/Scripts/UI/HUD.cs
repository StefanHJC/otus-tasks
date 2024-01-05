using UnityEngine.UI;

namespace ShootEmUp
{
    public class HUD
    {
        private readonly GameplayButton _startButton;
        private readonly GameplayButton _resumeButton;
        private readonly GameplayButton _pauseButton;
        private readonly ScreenCounter _screenCounter;

        public HUD(GameplayButton startButton, GameplayButton resumeButton, GameplayButton pauseButton, ScreenCounter screenCounter)
        {
            _startButton = startButton;
            _resumeButton = resumeButton;
            _pauseButton = pauseButton;
            _screenCounter = screenCounter;
        }
    }
}