using System;
using UnityEngine;

public class PortalActivator : MonoBehaviour
{
    [SerializeField] private RunePlace[] _runePlaces;
    [SerializeField] private GameObject _portal;
    [SerializeField] private MeshDissolver _dissolver;
    [SerializeField] private GameCompleter _gameCompleter;
    [SerializeField] private float _rotationSpeed;

    private int _filledPlaces;
    private Collider _collider;

    private void OnEnable()
    {
        foreach (var runPlace in _runePlaces)
        {
            runPlace.OnRunePlaced += FillRunePlace;
        }
    }

    private void OnDisable()
    {
        foreach (var runPlace in _runePlaces)
        {
            runPlace.OnRunePlaced -= FillRunePlace;
        }
    }

    private void Start()
    {
        _filledPlaces = 0;
        _collider = _portal.GetComponent<Collider>();
    }

    private void FillRunePlace()
    {
        _filledPlaces++;

        TryOpenPortal();
    }

    private void TryOpenPortal()
    {
        if (_filledPlaces >= _runePlaces.Length)
        {
            _dissolver.DissolveObject();
            _collider.enabled = false;
            _gameCompleter.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        transform.Rotate(transform.forward, _rotationSpeed * Time.deltaTime);
    }
}