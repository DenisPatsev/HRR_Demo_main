using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _fovChangeSpeed;

    private Camera _camera;
    private Coroutine _fovChangeCoroutine;

    private void Start()
    {
        _camera = Camera.main;
        _camera.fieldOfView = 60f;
    }

    public void ChangeFov(float fov, float speed)
    {
        if (_fovChangeCoroutine != null)
            StopCoroutine(_fovChangeCoroutine);

        _fovChangeCoroutine = StartCoroutine(ChangeFOV(fov, speed));
    }

    private IEnumerator ChangeFOV(float targetFOV, float speed)
    {
        while (Mathf.Abs(_camera.fieldOfView - targetFOV) > 0.05f)
        {
            _camera.fieldOfView = Mathf.MoveTowards(_camera.fieldOfView, targetFOV, Mathf.Pow(Time.deltaTime * speed, 2));
            yield return null;
        }
    }
}