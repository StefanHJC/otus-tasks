using UnityEngine;

namespace ShootEmUp
{
    public sealed class AssetProvider : IService
    {
        public T Instantiate<T>(T prefab)  where T : Component => Object.Instantiate(prefab);

        public GameObject Instantiate(GameObject prefab) => Object.Instantiate(prefab);

        public void Destroy(GameObject gameObject) => Object.Destroy(gameObject);
    }
}