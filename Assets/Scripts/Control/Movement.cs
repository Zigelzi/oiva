using Oiva.Discovery;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Control
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        [SerializeField] float _maxSpeed = 6f;
        ParkingSpot _parkingSpot;
        PlayerInputActions _playerInputActions;
        InputAction _movementInput;
        Rigidbody _rb;

        Vector3 _inputValue = Vector3.zero;
        Vector3 _movementForce = Vector3.zero;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _rb = GetComponent<Rigidbody>();
            _parkingSpot = _parkingSpot = GameObject.FindGameObjectWithTag("Parking_Spot").GetComponent<ParkingSpot>();
        }

        private void OnEnable()
        {
            _movementInput = _playerInputActions.Player.Move;
            _movementInput.Enable();

            _parkingSpot.onAllScootersParked.AddListener(DisableMovement);
        }

        private void Update()
        {
            GetInput();
        }

        private void FixedUpdate()
        {
            Move();
            LookForward();
            UpdateAnimation();
        }

        private void OnDisable()
        {
            _movementInput.Disable();
            _parkingSpot.onAllScootersParked.RemoveListener(DisableMovement);
        }

        private void DisableMovement()
        {
            _rb.velocity = Vector3.zero;
            UpdateAnimation();
            enabled = false;
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

        void UpdateAnimation()
        {
            Animator playerAnimator = GetComponent<Animator>();
            Vector3 playerVelocity = _rb.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);
            float currentSpeed = localVelocity.z;

            playerAnimator.SetFloat("forwardSpeed", currentSpeed);

        }
    }
}
