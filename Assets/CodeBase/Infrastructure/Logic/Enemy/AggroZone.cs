using System;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    public EnemyMover _enemyMover;

    private Enemy _enemy;
    private NoiseChecker _noiseChecker;
    private Player _player;

    private bool _isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
        }

        if (other.TryGetComponent(out NoiseChecker noiseChecker))
        {
            _noiseChecker = noiseChecker;
            _noiseChecker.enabled = true;
            _noiseChecker.ShowIcon();
            _noiseChecker.SetTargetColor(Color.red);
            _noiseChecker.OnNoiseFilled += SetTriggeredState;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_noiseChecker != null)
        {
            if (_noiseChecker != null && Input.GetAxis("Vertical") > 0.3f || Input.GetAxis("Horizontal") > 0.3f)
                _noiseChecker.SetTargetColor(Color.red);
            else
                _noiseChecker.SetTargetColor(Color.white);
        }

        if (_isTriggered)
        {
            _enemyMover.SetTriggeredState(_player.transform.position);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (_noiseChecker != null)
        {
            _noiseChecker.SetTargetColor(Color.white);
            _noiseChecker.HideIcon();
            _enemyMover.StartCooldown();
            _noiseChecker.OnNoiseFilled -= SetTriggeredState;
            _isTriggered = false;
        }
    }

    private void SetTriggeredState()
    {
        _isTriggered = true;

        Debug.LogError("TRIGGERED");
    }
}