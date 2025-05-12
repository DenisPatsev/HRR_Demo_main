using UnityEngine;

namespace CodeBase.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            Debug.Log(prefab);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}