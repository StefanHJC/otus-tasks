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

        public IHUD InstantiateHUD() => _assets.Instantiate(_data.Hud);
    }
}