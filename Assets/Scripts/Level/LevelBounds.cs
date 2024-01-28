using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class LevelBounds 
    {
        private readonly Level _level;
    
        [Inject]
        public LevelBounds(LevelProvider provider)
        {
            _level = provider.Level;
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            
            return positionX > _level.LeftBorder.x && 
                   positionX < _level.RightBorder.x && 
                   positionY > _level.DownBorder.y && 
                   positionY < _level.TopBorder.y;
        }
    }
}