using CodeBase.Infrastructure.Logic.InteractionService;
using UnityEngine;

public class Torch : InteractableObject
{
    private Rigidbody _rigidbody;
    private string _playerLayerName;
    private string _interactableLayerName;

    protected override void Start()
    {
        base.Start();
        
        _rigidbody = GetComponent<Rigidbody>();
        _playerLayerName = "Player";
        _interactableLayerName = "InteractableObject";
    }

    public void Throw()
    {
        _rigidbody.isKinematic = false;
        transform.parent = null;
        _rigidbody.AddForce(transform.forward * 3f, ForceMode.Impulse);
        gameObject.layer = LayerMask.NameToLayer(_interactableLayerName);
    }
}