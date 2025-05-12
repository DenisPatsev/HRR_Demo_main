using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        
        public SphereCollider collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _saveLoadService.SaveProgress();
                
                Debug.Log("Progress Saved");
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (!collider)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, collider.radius);
        }
    }
}