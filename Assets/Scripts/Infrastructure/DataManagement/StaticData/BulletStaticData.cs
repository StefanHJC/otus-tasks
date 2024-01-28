using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Static data/New bullet static data")]
    public class BulletStaticData : ScriptableObject
    {
        public List<BulletPrefabData> PrefabData;


        [System.Serializable]
        public struct BulletPrefabData
        {
            public BulletTypeId TypeId;
            public GameObject Prefab;
        }
    }
}