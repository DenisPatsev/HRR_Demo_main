using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _dissolveDuration = 3f;
    [SerializeField] MeshDissolver _meshDissolver;

    private Color _startColor;

    private float _animationStartDelay;

    private MaterialPropertyBlock[] _materialPropertyBlocks;

    public event UnityAction OnDeath;

    private void Start()
    {
        _startColor = Color.green;
        _animationStartDelay = Random.Range(0f, 2f);
        StartCoroutine(StartAnimation());
        _materialPropertyBlocks = new MaterialPropertyBlock[_renderers.Length];
    }

    public void Attack()
    {
    }

    public void Die()
    {
        _meshDissolver.DissolveObject();
    }

    private IEnumerator StartAnimation()
    {
        float timer = 0;

        while (timer < _animationStartDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _animator.enabled = true;
    }
}