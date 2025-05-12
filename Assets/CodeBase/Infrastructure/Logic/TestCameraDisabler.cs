using UnityEngine;

public class TestCameraDisabler : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}