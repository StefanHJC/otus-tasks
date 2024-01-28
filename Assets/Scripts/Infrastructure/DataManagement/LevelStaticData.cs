using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class LevelStaticData : ScriptableObject
    {
        public List<LevelPrefabData> PrefabData;

        [System.Serializable]
        public struct LevelPrefabData
        {
            public int Index;
            public Level LevelData;
        }
    }
}