using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class LevelBounds 
    {
        private Level _level;
    
        [Inject]
        public LevelBounds(LevelProvider provider)
        {
            LazyInitAsync(provider);
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            
            return positionX > _level.LeftBorder.position.x && 
                   positionX < _level.RightBorder.position.x && 
                   positionY > _level.DownBorder.position.y && 
                   positionY < _level.TopBorder.position.y;
        }

        private async void LazyInitAsync(LevelProvider provider)
        {
            while (provider.Level == null)
                await Task.Yield();

            _level = provider.Level;
        }
    }
}