using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string IsPistol = "isPistol";
    private const string IsObject = "isObject";
    private const string IsZoomOn = "isZoomOn";
    private const string IsRun = "IsRun";

    [SerializeField] private Animator _playerAnimator;

    public Animator PlayerAnimator => _playerAnimator;
    

    public void SetStartAnimation()
    {
        ResetAllAnimations();

        _playerAnimator.enabled = true;
    }

    public void SetObjectHoldingAnimation()
    {
        ResetAllAnimations();
        _playerAnimator.SetBool(IsObject, true);
        _playerAnimator.enabled = true;
    }

    public void SetRunState(bool isRun)
    {
        _playerAnimator.SetBool(IsRun, isRun);
    }
    
    private void ResetAllAnimations()
    {
        _playerAnimator.SetBool(IsPistol, false);
        _playerAnimator.SetBool(IsObject, false);
        _playerAnimator.enabled = false;
    }
}