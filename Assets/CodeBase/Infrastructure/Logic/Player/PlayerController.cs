using CodeBase.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ISavedProgress
{
    private const string GroundLayerName = "Ground";

    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _groundChecker;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _groundMinimalHeight;
    
    private float _rotationX;
    private float _rotationY;
    private float _currentRotationX;
    private float _currentRotationY;
    private float _shootTimer;
    private float _defaultMoveSpeed;
    private float _runSpeed;
    private float _speedMultiplier;
    private Vector3 _velocity;

    public event UnityAction SneakOn;
    public event UnityAction SneakOff;

    private void Start()
    {
        _defaultMoveSpeed = _moveSpeed;
        _speedMultiplier = 2f;
        _runSpeed = _moveSpeed * _speedMultiplier;
    }

    private void Update()
    {
        Rotate();
        Move();
        Sneak();
        Run();
        Jump();
        UseGravity();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _rotationX += mouseX;
        _rotationY += -mouseY;

        _rotationY = Mathf.Clamp(_rotationY, -90, 90);
        _currentRotationX = Mathf.Lerp(_currentRotationX, _rotationX, _rotationSpeed * Time.deltaTime);
        _currentRotationY = Mathf.Lerp(_currentRotationY, _rotationY, _rotationSpeed * Time.deltaTime);
        float rotationZ = Mathf.Lerp(_body.transform.localRotation.z, -4 * mouseX,
            _rotationSpeed * Time.deltaTime * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, _currentRotationX, 0);
        _body.transform.localRotation = Quaternion.Euler(_currentRotationY, 0, rotationZ);
    }

    private void Move()
    {
        Vector3 direction = (Input.GetAxis("Vertical") * transform.forward +
                             Input.GetAxis("Horizontal") * transform.right);

        _characterController.Move(direction.normalized * _moveSpeed * Time.deltaTime);
    }

    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0.1f)
        {
            _playerAnimationController.SetRunState(true);
            _moveSpeed = _runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _playerAnimationController.SetRunState(false);
            _moveSpeed = _defaultMoveSpeed;
        }
    }

    private void Sneak()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _moveSpeed /= 2f;
            SneakOn?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            _moveSpeed *= 2f;
            SneakOff?.Invoke();
        }
    }

    private void UseGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGround())
            {
                Debug.Log("Jump");
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }
    }

    private bool CheckGround()
    {
        bool isGrounded = Physics.CheckSphere(_groundChecker.transform.position, _groundMinimalHeight,
            LayerMask.GetMask(GroundLayerName));
        return isGrounded;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());
    }

    private string GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (GetCurrentLevel() == progress.WorldData.PositionOnLevel.LevelName)
        {
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

            if (savedPosition != null)
                Warp(savedPosition);
        }
    }

    private void Warp(Vector3Data savedPosition)
    {
        _characterController.enabled = false;
        transform.position = savedPosition.AsUnityVector();
        _characterController.enabled = true;
    }
}