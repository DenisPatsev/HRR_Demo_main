using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}