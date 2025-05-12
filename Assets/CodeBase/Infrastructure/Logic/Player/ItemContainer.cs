using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Logic.InteractionService;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private List<InteractableObject> _items = new List<InteractableObject>();

    public void AddItem(InteractableObject item)
    {
        _items.Add(item);
        Debug.Log(_items.Last().name);
    }

    public TItem GetItem<TItem>() where TItem : class, IInteractable
    {
        TItem result = null;
        
        foreach (var item in _items)
        {
            if (item.TryGetComponent(out TItem tItem))
            {
                result = tItem;
                _items.Remove(item);
                break;
            }
        }
        
        return result;
    }
}
