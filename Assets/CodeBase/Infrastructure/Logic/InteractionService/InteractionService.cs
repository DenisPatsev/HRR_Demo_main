using UnityEngine;

public class InteractionService : MonoBehaviour
{
    [SerializeField] private GameObject _interactableObjectPlace;

    public GameObject InteractableObjectPlace => _interactableObjectPlace;

    private IInteractable _objectInHand;
    public IInteractable ObjectInHand => _objectInHand;

    public void SetObjectInHand(IInteractable interactableObject)
    {
        _objectInHand = interactableObject;
    }
}