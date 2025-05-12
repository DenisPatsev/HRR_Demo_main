using System.Collections;
using CodeBase.Infrastructure.Logic.InteractionService;
using UnityEngine;

public class Door : InteractableObject
{
    public float startRotationY;
    public float targetRotationY;
    public float rotationSpeed;

    private bool _isOpened;
    private float _currentRotationY;

    private Coroutine _openingCoroutine;

    protected override void Start()
    {
        base.Start();

        _isOpened = false;
        transform.localEulerAngles = new Vector3(0, startRotationY, 180);
        _currentRotationY = transform.localEulerAngles.y;
    }

    public override void Interact(Player player)
    {
        _isOpened = !_isOpened;
        
        if (_openingCoroutine != null)
            StopCoroutine(_openingCoroutine);

        _openingCoroutine = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        if (!_isOpened)
        {
            float rotation = targetRotationY;
            while (_currentRotationY < rotation - 1)
            {
                RotateSmoothly(rotation);
                yield return null;
            }
        }
        else
        {
            float rotation = startRotationY;
            while (_currentRotationY > rotation + 1)
            {
                RotateSmoothly(rotation);
                yield return null;
            }
        }
    }

    private void RotateSmoothly(float targetRotation)
    {
        _currentRotationY = Mathf.MoveTowards(_currentRotationY, targetRotation, rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, _currentRotationY, 180);
    }
}