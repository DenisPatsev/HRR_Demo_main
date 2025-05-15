using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _zOffset;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minimalDistance;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _idleCooldown;

    private Enemy _enemy;
    
    private Vector3 _newPosition;
    private Quaternion _targetRotation;
    private Coroutine _cooldownCoroutine;

    private float _currentRotation;
    private float _aggroMoveSpeed;
    private float _defaultMoveSpeed;
    private float _speedMultiplier;

    private enum States
    {
        Idle,
        Triggered
    }

    private States _currentState;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.OnDeath += StopMoving;
    }

    private void OnDisable()
    {
        _enemy.OnDeath -= StopMoving;
    }

    private void Start()
    {
        GenerateRandomPosition();
        _defaultMoveSpeed = _moveSpeed;
        _speedMultiplier = 1.15f;
        _aggroMoveSpeed = _defaultMoveSpeed * _speedMultiplier;
        _currentState = States.Idle;
    }

    private void Update()
    {
        MoveToNewPosition();
    }

    public void StartCooldown()
    {
        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);

        _cooldownCoroutine = StartCoroutine(StartIdleCooldown());
    }

    public void SetTriggeredState(Vector3 playerPosition)
    {
        _currentState = States.Triggered;

        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);

        _newPosition = playerPosition;
        _targetRotation = Quaternion.LookRotation(_newPosition - transform.position);
        _moveSpeed = _aggroMoveSpeed;
    }

    private void GenerateRandomPosition()
    {
        if (_currentState == States.Idle)
        {
            float randomX = Random.Range(-_xOffset, _xOffset);
            float randomZ = Random.Range(-_zOffset, _zOffset);

            _newPosition = new Vector3(randomX + transform.position.x, transform.position.y,
                randomZ + transform.position.z);
            Vector3 direction = _newPosition - transform.position;
            _targetRotation = Quaternion.LookRotation(direction);
        }
    }

    private void StopMoving()
    {
        _moveSpeed = 0f;
    }

    private void MoveToNewPosition()
    {
        Move();

        if (Vector3.Distance(transform.position, _newPosition) < _minimalDistance)
        {
            GenerateRandomPosition();
        }
    }

    private IEnumerator StartIdleCooldown()
    {
        float elapsedTime = 0;
        _moveSpeed /= 2f;

        while (elapsedTime < _idleCooldown)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        SetIdleState();
    }

    private void SetIdleState()
    {
        _currentState = States.Idle;
        GenerateRandomPosition();
        _moveSpeed = _defaultMoveSpeed;
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _moveSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
    }
}