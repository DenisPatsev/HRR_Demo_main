using System;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    public EnemyMover _enemyMover;

    private Enemy _enemy;
    private Player _player;

    private bool _isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_player != null)
            _enemyMover.SetTriggeredState(_player.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemyMover.StartCooldown();
    }
}