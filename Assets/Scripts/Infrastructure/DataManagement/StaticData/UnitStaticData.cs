using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Static data/New unit static data")]
    public class UnitStaticData : ScriptableObject
    {
        public List<UnitPrefabData> PrefabData;

        [System.Serializable]
        public struct UnitPrefabData
        {
            public UnitTypeId TypeId;
            public GameObject Prefab;
            public BulletConfig BulletConfig;
        }
    }
}