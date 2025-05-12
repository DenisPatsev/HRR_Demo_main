using UnityEngine;

public class BodyAnimator : MonoBehaviour
{
    public BodyPart[] bodyParts;

    public void Update()
    {
        foreach (var part in bodyParts)
        {
            part.SetPartRotation();
        }
    }
}