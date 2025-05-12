using System.Collections;
using UnityEngine;

public class MeshDissolver : MonoBehaviour
{
    [SerializeField] Material _dissolveMaterial;
    [SerializeField] Renderer[] _renderers;
    [SerializeField] float _dissolveSpeed;
    [SerializeField] float _dissolveDisablingDuration;
        
    private MaterialPropertyBlock[] _materialPropertyBlocks;
    private Coroutine _dissolveCoroutine;

    public void DissolveObject()
    {
        if(_dissolveCoroutine != null)
            StopCoroutine(_dissolveCoroutine);

        _dissolveCoroutine = StartCoroutine(PortalDisabler());
    }
        
    private IEnumerator PortalDisabler()
    {
        _materialPropertyBlocks = new MaterialPropertyBlock[_renderers.Length];
        
        for (int i = 0; i < _renderers.Length; i++)
        {
            _materialPropertyBlocks[i] = new MaterialPropertyBlock();
            _renderers[i].material = _dissolveMaterial;
        }
        _dissolveMaterial.SetFloat("_DissolveThreshold", 0);

        float dissolveProgress = 0;
        float thresholdDelta = 0;

        while (dissolveProgress < _dissolveDisablingDuration)
        {
            for (int i = 0; i < _materialPropertyBlocks.Length; i++)
            {
                _materialPropertyBlocks[i].SetFloat("_DissolveThreshold", thresholdDelta);
                _renderers[i].SetPropertyBlock(_materialPropertyBlocks[i]);
            }

            thresholdDelta += Time.deltaTime * _dissolveSpeed;
            dissolveProgress += Time.deltaTime;
            yield return null;
        }

        Debug.LogError("Mesh dissolved");

        gameObject.SetActive(false);
    }
}