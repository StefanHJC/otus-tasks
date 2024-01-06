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

    public class GameplayButton : ISceneEntity, IUIText, IClickable, ISwitchable
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

        public GameplayButton(Button button, string text=default, Vector2 position=default)
        {
            _button = button;
            _buttonText = button.GetComponentInChildren<TMP_Text>();
            //Text = text;
            //Position = position;

            _button.onClick.AddListener( () => Clicked?.Invoke());
        }

        public void Enable() => _button.gameObject.SetActive(true);

        public void Disable() => _button.gameObject.SetActive(false);
    }
}