using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public interface IUIText
    {
        public string Text { get; set; }
    }

    public interface IClickable
    {
        event Action Clicked;
    }

    public class GameplayButton : ISceneEntity, 
        IUIText, 
        IClickable, 
        ISwitchable,
        IDisposable
    {
        private readonly Button _button;
        private readonly TMP_Text _buttonText;

        public string Text 
        { 
            get => _buttonText.text; 
            set => _buttonText.text = value;
        }

        public Vector2 Position 
        { 
            get => _button.transform.position;
            set => _button.transform.position = value;
        }

        public event Action Clicked;

        public GameplayButton(Button button)
        {
            _button = button;
            _buttonText = button.GetComponentInChildren<TMP_Text>();

            _button.onClick.AddListener(InvokeClickEvent);
        }


        public void Enable() => _button.gameObject.SetActive(true);

        public void Disable() => _button.gameObject.SetActive(false);

        private void InvokeClickEvent() => Clicked?.Invoke();
        
        public void Dispose() => _button.onClick.RemoveListener(InvokeClickEvent);
    }
}