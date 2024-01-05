using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class ScreenCounter : ISceneEntity, IUIText, ISwitchable
    {
        private readonly TMP_Text _counter;

        public Vector2 Position
        {
            get => _counter.rectTransform.position; 
            set => _counter.rectTransform.position = value;
        }

        public string Text 
        { 
            get => _counter.text; 
            set => _counter.text = value;
        }

        public ScreenCounter(TMP_Text text)
        {
            _counter = text;
        }

        public void Enable() => _counter.gameObject.SetActive(true);

        public void Disable() => _counter.gameObject.SetActive(false);
    }
}