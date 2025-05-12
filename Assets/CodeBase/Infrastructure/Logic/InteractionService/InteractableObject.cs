using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.InteractionService
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public string objectName;
        public InteractableObjectType objectType;

        protected string _interactionDescription;

        public string InteractionDescription => _interactionDescription;

        protected virtual void Start()
        {
            SetInteractionType();
        }

        protected void SetInteractionType()
        {
            switch (objectType)
            {
                case InteractableObjectType.Interact:
                    _interactionDescription = "Взаимодействовать";
                    break;
                case InteractableObjectType.Get:
                    _interactionDescription = "Взять";
                    break;
            }
        }

        public virtual void Interact(Player player)
        {
        }
    }
}