using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Static data/New game static data")]
    public class GameStaticData : ScriptableObject
    {
        [Min(0)]
        public int GameStartDelayInSeconds;
        public InputSchema InputSchema;
    }
}