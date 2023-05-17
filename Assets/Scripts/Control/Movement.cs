using UnityEngine;
using UnityEngine.InputSystem;
namespace Oiva.Control
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        [SerializeField] float _maxSpeed = 6f;

        PlayerInputActions _playerInputActions;
        InputAction _movementInput;
        Rigidbody _rb;

        Vector3 _inputValue = Vector3.zero;
        Vector3 _movementForce = Vector3.zero;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _movementInput = _playerInputActions.Player.Move;
            _movementInput.Enable();
        }

        private void Update()
        {
            GetInput();
        }

        private void FixedUpdate()
        {
            Move();
            LookForward();
        }

        private void OnDisable()
        {
            _movementInput.Disable();
        }

        private void GetInput()
        {
            _inputValue = new Vector3(_movementInput.ReadValue<Vector2>().x, 0, _movementInput.ReadValue<Vector2>().y);
        }

        private void Move()
        {
            if (_rb == null) return;

            _movementForce += _inputValue.x * GetCameraRight() * _speed;
            _movementForce += _inputValue.z * GetCameraForward() * _speed;

            _rb.AddForce(_movementForce, ForceMode.Impulse);
            _movementForce = Vector3.zero;

            LimitToMaxSpeed();
        }

        private void LimitToMaxSpeed()
        {
            Vector3 playerVelocity = _rb.velocity;
            playerVelocity.y = 0;
            if (playerVelocity.sqrMagnitude > _maxSpeed * _maxSpeed)
            {
                _rb.velocity = playerVelocity.normalized * _maxSpeed + Vector3.up * _rb.velocity.y;
            }
        }

        private void LookForward()
        {
            Vector3 relativePosition = (transform.position - _inputValue) - transform.position;
            if (relativePosition == Vector3.zero) return;

            Quaternion lookRotation = Quaternion.LookRotation(-relativePosition, Vector3.up);
            transform.rotation = lookRotation;

        }

        private Vector3 GetCameraForward()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            return cameraForward.normalized;
        }

        private Vector3 GetCameraRight()
        {
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            return cameraRight.normalized;
        }

    }
}
