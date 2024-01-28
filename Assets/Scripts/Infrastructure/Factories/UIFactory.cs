using System.Linq;
using Zenject;

namespace ShootEmUp
{
    public class UIFactory
    {
        private readonly UIStaticData _data;
        private readonly UIProvider _provider;
        private readonly IAssetProvider _assets;

        [Inject]
        public UIFactory(IDatabaseService data, IAssetProvider assets, UIProvider provider)
        {
            _data = data.Get<UIStaticData>().FirstOrDefault();
            _assets = assets;
            _provider = provider;
        }

        public IHUD InstantiateHUD()
        {
            IHUD hudInstance = _assets.Instantiate(_data.Hud); ; 

            _provider.Hud ??= hudInstance;

            return hudInstance;
        }
    }
}