using System.Linq;
using Zenject;

namespace ShootEmUp
{
    public class UIFactory
    {
        private readonly UIProvider _provider;
        private readonly IDatabaseService _data;
        private readonly IAssetProvider _assets;

        [Inject]
        public UIFactory(IDatabaseService data, IAssetProvider assets, UIProvider provider)
        {
            _data = data;
            _assets = assets;
            _provider = provider;
        }

        public IHUD InstantiateHUD()
        {
            IHUD hudInstance = _assets.Instantiate(_data.Get<UIStaticData>().FirstOrDefault().Hud); ; 

            _provider.Hud ??= hudInstance;

            return hudInstance;
        }
    }
}