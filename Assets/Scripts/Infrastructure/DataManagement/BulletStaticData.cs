using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletStaticData : ScriptableObject
    {
        public BulletPrefabData PrefabData;

        [System.Serializable]
        public struct BulletPrefabData
        {
            public Dictionary<BulletTypeId, GameObject> Value;
        }
    }
}