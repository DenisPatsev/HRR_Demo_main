using System;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] private Shader _fogShader;
    [SerializeField] private Color _fogColor;
    [Range(0, 1)] public float _fogDensity;
    [Range(0, 1f)] public float _fogEnd;

    private Material _material;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.depthTextureMode = DepthTextureMode.Depth;
        _material = new Material(_fogShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _material.SetColor("_FogColor", _fogColor);
        _material.SetFloat("_FogDensity", _fogDensity);
        _material.SetFloat("_FogEnd", _fogEnd);
        Graphics.Blit(source, destination, _material, 0);
    }

    private void OnDisable()
    {
        mainCamera.depthTextureMode = DepthTextureMode.None;
    }
}