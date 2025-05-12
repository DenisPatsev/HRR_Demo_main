using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class NoiseChecker : MonoBehaviour
{
    public PlayerController playerController;
    public float noiseScaleSpeed;
    public float iconActivationSpeed;
    private Coroutine _showCoroutine;
    private UIDocument _hud;
    private VisualElement _noiseIcon;

    private Color _triggeredColor;
    private Color _targetColor;

    public event UnityAction OnNoiseFilled;

    private void OnEnable()
    {
        noiseScaleSpeed = 2.0f;
        playerController.SneakOn += ReduceNoise;
        playerController.SneakOff += ReceiveNoise;
    }

    private void OnDisable()
    {
        playerController.SneakOn -= ReduceNoise;
        playerController.SneakOff -= ReceiveNoise;
    }

    private void Start()
    {
        _triggeredColor = new Color(0.95f, 0, 0, 1f);
        _targetColor = _triggeredColor;
    }

    private void Update()
    {
        if (_noiseIcon == null)
            return;

        _noiseIcon.style.unityBackgroundImageTintColor = Color.Lerp(
            _noiseIcon.resolvedStyle.unityBackgroundImageTintColor, _targetColor, noiseScaleSpeed * Time.deltaTime);

        if (_noiseIcon.resolvedStyle.unityBackgroundImageTintColor.g < 0.1f)
        {
            OnNoiseFilled?.Invoke();
        }
    }

    public void InitializeIndicator(UIDocument hud)
    {
        _hud = hud;
        _noiseIcon = _hud.rootVisualElement.Q<VisualElement>("NoiseIndicator");
    }

    public void SetTargetColor(Color color)
    {
        _targetColor = color;
    }

    public void ShowIcon()
    {
        StopActiveCoroutine();

        _showCoroutine = StartCoroutine(IconShower());
    }

    public void HideIcon()
    {
        StopActiveCoroutine();
        _showCoroutine = StartCoroutine(IconHider());
    }

    private void StopActiveCoroutine()
    {
        if (_showCoroutine != null)
            StopCoroutine(_showCoroutine);
    }

    private IEnumerator IconShower()
    {
        _noiseIcon.style.opacity = 0f;
        
        while (_noiseIcon.resolvedStyle.opacity < 0.95f)
        {
            _noiseIcon.style.opacity =
                Mathf.Lerp(_noiseIcon.resolvedStyle.opacity, 1, iconActivationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator IconHider()
    {
        while (_noiseIcon.resolvedStyle.opacity > 0.01f)
        {
            _noiseIcon.style.opacity =
                Mathf.Lerp(_noiseIcon.resolvedStyle.opacity, 0, iconActivationSpeed * Time.deltaTime);
            yield return null;
        }

        _noiseIcon.style.opacity = 0;
        enabled = false;
    }
    
    private void ReduceNoise()
    {
        noiseScaleSpeed /= 3.2f;
    }

    private void ReceiveNoise()
    {
        noiseScaleSpeed *= 3.2f;
    }
}