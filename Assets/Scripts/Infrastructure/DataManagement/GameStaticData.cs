using UnityEngine;

namespace ShootEmUp
{
    public class GameStaticData : ScriptableObject
    {
        [Min(0)]
        public int GameStartDelayInSeconds;
        public InputSchema InputSchema;
    }
}