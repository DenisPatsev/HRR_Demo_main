using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace CodeBase.Infrastructure
{
    public class GameCompass : MonoBehaviour
    {
        public float rotationSpeed;
        
        private VisualElement _compassArrow;
        private float _targetRotation;
        private float _currentRotation;
        
        public VisualElement ComassArrow => _compassArrow;

        private void Start()
        {
            _currentRotation = 0;
        }

        private void Update()
        {
            if (_compassArrow == null)
                return;
            
            _currentRotation -= Input.GetAxis("Mouse X");
            _targetRotation = Mathf.Lerp(_targetRotation, _currentRotation, rotationSpeed * Time.deltaTime);
            Rotate(_compassArrow, _targetRotation);
        }

        public void InitializeCompass(UIDocument hud)
        {
            _compassArrow = hud.rootVisualElement.Q<VisualElement>("CompassArrow");
        }

        private void Rotate(VisualElement element, float rotation)
        {
            var transform = element.transform;
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            element.transform.rotation = transform.rotation;
        }
    }
}