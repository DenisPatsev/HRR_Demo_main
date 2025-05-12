using System;
using CodeBase.Infrastructure.Logic.InteractionService;
using UnityEngine;

internal class RunePlace : InteractableObject
{
    public Collider collider;
    public event Action OnRunePlaced;
    public override void Interact(Player player)
    {
        var container = player.GetComponent<ItemContainer>();

        var rune = container.GetItem<Rune>();
        rune.gameObject.SetActive(true);
        rune.transform.position = transform.position;
        rune.transform.parent = transform;
        OnRunePlaced?.Invoke();
        collider.enabled = false;
        enabled = false;
    }
}