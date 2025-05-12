using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public GameObject Instantiate(string path, Vector3 position);
    }
}