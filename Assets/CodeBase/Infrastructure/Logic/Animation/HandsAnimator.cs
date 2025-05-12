using UnityEngine;

public class HandsAnimator : MonoBehaviour
{
    public AnimatedHand _leftHand;
    public AnimatedHand _rightHand;

    private void Update()
    {
        SetNewFingerRotation(_leftHand);
        SetNewFingerRotation(_rightHand);
    }

    private void SetNewFingerRotation(AnimatedHand hand)
    {
        foreach (var finger in hand.Fingers)
        {
            finger.SetPartsRotation(hand.gripMultiplier);
        }
    }
}