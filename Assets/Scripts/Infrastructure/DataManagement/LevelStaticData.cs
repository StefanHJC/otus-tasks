using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static data/New level static data")]
    public class LevelStaticData : ScriptableObject
    {
        public List<LevelPrefabData> PrefabData;

        [System.Serializable]
        public struct LevelPrefabData
        {
            public int Index;
            public Level Prefab;
        }
    }
}