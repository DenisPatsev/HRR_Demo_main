using UnityEngine;

public class AmbientOcclusion : MonoBehaviour
{
    [Range(0, 5)] public float occlusionStrength = 1.0f;
    [Range(0, 1)] public float occlusionThreshold = 1.0f;
    [Range(1, 64)] public int sampleCount = 16;

    public Shader aoShader;
    private Material aoMaterial;

    private Camera mainCamera;

    private void Start()
    {
        enabled = false;
    }
    
    void OnEnable()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.depthTextureMode |= DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (aoShader == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        if (aoMaterial == null)
        {
            aoMaterial = new Material(aoShader);
            aoMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        aoMaterial.SetFloat("_OcclusionStrength", occlusionStrength);
        aoMaterial.SetFloat("_OcclusionThreshold", occlusionThreshold);
        aoMaterial.SetInt("_Iterations", sampleCount);

        Graphics.Blit(source, destination, aoMaterial);
    }

    void OnDisable()
    {
        if (aoMaterial != null)
            DestroyImmediate(aoMaterial);
        
        mainCamera.depthTextureMode &= ~DepthTextureMode.Depth;
    }
}