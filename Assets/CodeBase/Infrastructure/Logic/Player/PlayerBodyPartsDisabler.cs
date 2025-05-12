using UnityEngine;

public class PlayerBodyPartsDisabler : MonoBehaviour
{
    [SerializeField] private GameObject[] _parts;

    private void Start()
    {
        SetPartsState(false);
    }

    public void SetPartsState(bool state)
    {
        foreach (var part in _parts)
        {
            part.gameObject.SetActive(state);
        }
    }
}
