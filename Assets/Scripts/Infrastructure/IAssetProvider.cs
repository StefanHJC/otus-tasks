using UnityEngine;

namespace ShootEmUp
{
    public interface IAssetProvider
    {
        T Instantiate<T>(T prefab)  where T : Component;
        GameObject Instantiate(GameObject prefab);
        void Destroy(GameObject gameObject);
    }
}