using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class UnitStaticData : ScriptableObject
    {
        public UnitPrefabData PrefabData;

        [System.Serializable]
        public struct UnitPrefabData
        {
            public Dictionary<UnitTypeId, GameObject> Value;
        }
    }
}