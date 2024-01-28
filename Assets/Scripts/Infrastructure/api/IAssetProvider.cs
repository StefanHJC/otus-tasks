using UnityEngine;

namespace ShootEmUp
{
    public interface IAssetProvider
    {
        T Instantiate<T>(T prefab)  where T : Object;
        GameObject Instantiate(GameObject prefab);
        void Destroy(GameObject gameObject);
    }
}