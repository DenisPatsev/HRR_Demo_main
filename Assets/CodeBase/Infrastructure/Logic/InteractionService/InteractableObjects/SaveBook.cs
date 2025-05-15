using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.InteractionService.InteractableObjects
{
    public class SaveBook : InteractableObject
    {
        public ParticleSystem particleSystem;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnDisable()
        {
            Debug.LogError("Save trigger disabled");
        }

        public override void Interact(Player player)
        {
            _saveLoadService.SaveProgress();
            particleSystem.Play();
            StartCoroutine(Disable());
        }

        private IEnumerator Disable()
        {
            while (particleSystem.IsAlive())
            {
                yield return null;
            }
            
            gameObject.SetActive(false);
        }
    }
}