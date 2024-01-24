using TMPro;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class UIFactory
    {
        private readonly UIStaticData _data;
        private readonly IAssetProvider _assets;

        [Inject]
        public UIFactory(IDatabaseService data, IAssetProvider assets)
        {
            _data = data.Get<UIStaticData>();
            _assets = assets;
        }

        public IHUD InstantiateHUD()
        {
            GameObject hudInstance = _assets.Instantiate(_data.Hud);

            GameplayButton[] buttons = hudInstance.GetComponentsInChildren<GameplayButton>(); // TEMP

            return new HUD(buttons[0], buttons[1], buttons[2], new ScreenTextRenderer(hudInstance.GetComponentInChildren<TMP_Text>()));
        }
    }
}