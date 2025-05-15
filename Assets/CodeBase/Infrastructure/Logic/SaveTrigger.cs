using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        public ParticleSystem[] effects;
        private ISaveLoadService _saveLoadService;
        
        public SphereCollider collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }
        
        private void OnDisable()
        {
            Debug.LogError("Save trigger disabled");
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _saveLoadService.SaveProgress();
                
                Debug.Log("Progress Saved");
                foreach (ParticleSystem effect in effects)
                {
                    effect.Play();
                }
                
                StartCoroutine(Disable());
            }
        }
        
        private IEnumerator Disable()
        {
            collider.enabled = false;
            
            foreach (ParticleSystem effect in effects)
            {
                while (effect.IsAlive())
                {
                    yield return null;
                }
            }
            
            gameObject.SetActive(false);
        }
    }
}