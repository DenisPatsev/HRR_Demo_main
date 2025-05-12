using UnityEngine;

public class Bloom : MonoBehaviour
{
    [Range(0, 5)]
    public float bloomThreshold = 0.7f;
    [Range(0, 0.8f)]
    public float bloomIntensity = 1.0f;
    [Range(0, 4)]
    public float blurOffset = 1.0f;
    [Range(1, 6)]
    public int iterations = 4;

    public Shader bloomShader;
    private Material bloomMaterial;
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (bloomShader == null || iterations < 1)
        {
            Graphics.Blit(source, destination);
            return;
        }

        if (bloomMaterial == null)
        {
            bloomMaterial = new Material(bloomShader);
            bloomMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        bloomMaterial.SetFloat("_BloomThreshold", bloomThreshold);
        bloomMaterial.SetFloat("_BloomIntensity", bloomIntensity);
        bloomMaterial.SetFloat("_BlurOffset", blurOffset);
        bloomMaterial.SetInt("_Iterations", iterations);

        // Временные RT для размытия
        RenderTexture rt1 = RenderTexture.GetTemporary(source.width , source.height , 0, source.format);
        RenderTexture rt2 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, source.format);

        // Pass 0: Bloom + Kawase Blur
        Graphics.Blit(source, rt1, bloomMaterial, 0);

        // Pass 1: Composite
        bloomMaterial.SetTexture("_BloomTex", rt1);
        Graphics.Blit(source, destination, bloomMaterial, 1);

        RenderTexture.ReleaseTemporary(rt1);
        RenderTexture.ReleaseTemporary(rt2);
    }
}
