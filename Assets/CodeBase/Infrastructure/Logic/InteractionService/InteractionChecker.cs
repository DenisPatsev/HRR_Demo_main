using System;
using CodeBase.Infrastructure.Logic.InteractionService;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionChecker : MonoBehaviour
{
    [SerializeField] private UIDocument _hud;
    [SerializeField] private Player _player;
    [SerializeField] private Collider _collider;

    private Label _action;
    private Label _objectName;
    private InteractableObject _targetObject;
    private VisualElement _InteractionDisplay;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(_targetObject);
            if (_targetObject != null)
            {
                _targetObject.Interact(_player);
                _targetObject = null;
                HideInteractionDisplay();
                RestartCollider();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject interactableObject))
        {
            ShowHUD();
            _targetObject = interactableObject;
            _action.text = _targetObject.InteractionDescription;
            _objectName.text = _targetObject.objectName;
            Debug.LogError(_action.text);
        }
    }

    private void RestartCollider()
    {
        _collider.enabled = false;
        _collider.enabled = true;
    }

    public void SetHud(UIDocument hud)
    {
        _hud = hud;
        _InteractionDisplay = _hud.rootVisualElement.Q<VisualElement>("InteractionDisplay");
        HideInteractionDisplay();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject interactableObject))
        {
            _targetObject = null;
            HideInteractionDisplay();
            Debug.LogError("Hud hided");
        }
    }

    private void ShowHUD()
    {
        _InteractionDisplay.style.opacity = 1f;
        _collider.enabled = true;
        GetUIElements();
    }

    private void HideInteractionDisplay()
    {
        _InteractionDisplay.style.opacity = 0f;
        _InteractionDisplay.MarkDirtyRepaint();
    }

    private void GetUIElements()
    {
        _action = _hud.rootVisualElement.Q<Label>("Action");
        _objectName = _hud.rootVisualElement.Q<Label>("ObjectName");
    }
}