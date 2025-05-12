using UnityEngine;
using UnityEngine.Serialization;

public class HandFinger : MonoBehaviour
{
    public GameObject[] parts;

    public float targetRotation;
    public Vector3 rotationMultipliers;

    public void SetPartsRotation(float strength)
    {
        float rotation = 0;
        
        foreach (var part in parts)
        {
            rotation = Mathf.Lerp(rotation, targetRotation, strength);
            part.transform.localEulerAngles = new Vector3(rotation * rotationMultipliers.x, rotation * rotationMultipliers.y, rotation * rotationMultipliers.z);
        }
    }
}



public class DynamicAnimator : MonoBehaviour
{
    [FormerlySerializedAs("handAnimator")] public AnimatedHand animatedHand;
}