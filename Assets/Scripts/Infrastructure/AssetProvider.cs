using UnityEngine;

namespace ShootEmUp
{
    public sealed class AssetProvider : IAssetProvider
    {
        public T Instantiate<T>(T prefab)  where T : Object => Object.Instantiate(prefab);

        public GameObject Instantiate(GameObject prefab) => Object.Instantiate(prefab);

        public void Destroy(GameObject gameObject) => Object.Destroy(gameObject);
    }
}