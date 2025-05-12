using CodeBase.Infrastructure.Logic.InteractionService;
using UnityEngine;

public class Rune : InteractableObject
{
    [SerializeField] private GameObject _sphere;
    [SerializeField] private float _sphereRotationSpeed;
    [SerializeField] private float _runeRotationSpeed;

    private Collider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    
    private void Update()
    {
        _sphere.transform.Rotate(Vector3.up * _sphereRotationSpeed * Time.deltaTime);
        _sphere.transform.Rotate(Vector3.right * _sphereRotationSpeed * 1.5f * Time.deltaTime);
        transform.Rotate(Vector3.right * _runeRotationSpeed * Time.deltaTime);
    }
    
    public override void Interact(Player player)
    {
        player.itemContainer.AddItem(this);
        _collider.enabled = false;
        gameObject.SetActive(false);
    }
}
