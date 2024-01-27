using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class HUD : MonoBehaviour, IHUD
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _screenTextRenderer;

        public GameplayButton StartButton {get; private set; }
        public GameplayButton ResumeButton{ get; private set; }
        public GameplayButton PauseButton { get; private set; }
        public ScreenTextRenderer ScreenTextRenderer { get; private set; }

        private void Awake()
        {
            StartButton = new GameplayButton(_startButton);
            ResumeButton = new GameplayButton(_resumeButton);
            PauseButton = new GameplayButton(_pauseButton);
            ScreenTextRenderer = new ScreenTextRenderer(_screenTextRenderer);

            StartButton.Clicked += OnStartFired;
            ResumeButton.Clicked += OnResumeFired;
            PauseButton.Clicked += OnPauseFired;
        }

        private void OnStartFired() => StartButton.Disable();

        private void OnResumeFired()
        {
            ResumeButton.Disable();
            PauseButton.Enable();
        }

        private void OnPauseFired()
        {
            PauseButton.Disable();
            ResumeButton.Enable();
        }
    }
}