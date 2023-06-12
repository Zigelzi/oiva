using Oiva.Discovery;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Control
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        [SerializeField] float _initialMaxSpeed = 6f;
        [SerializeField] float _movementTolerance = .2f;

        float _currentMaxSpeed = -1f;
        Energy _energy;
        ParkingSpot _parkingSpot;
        PlayerInputActions _playerInputActions;
        Animator _playerAnimator;
        InputAction _movementInput;
        Rigidbody _rb;

        Vector3 _inputValue = Vector3.zero;
        Vector3 _movementForce = Vector3.zero;

        // Prototyping effects
        // Refactor these to use strategy pattern if useful
        [SerializeField] float _buffDuration = 5f;
        [SerializeField] float _additionalSpeed = 3f;
        Carrying _carrying;
        Coroutine _currentMovementEffect;

        private void Awake()
        {
            _currentMaxSpeed = _initialMaxSpeed;

            _carrying = GetComponent<Carrying>();
            _energy = GetComponent<Energy>();
            _playerInputActions = new PlayerInputActions();
            _playerAnimator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _parkingSpot = _parkingSpot = GameObject.FindGameObjectWithTag("Parking_Spot").GetComponent<ParkingSpot>();
        }

        private void OnEnable()
        {
            _movementInput = _playerInputActions.Player.Move;
            _movementInput.Enable();

            _carrying.onScooterParked.AddListener(AddMoveSpeed);
            _energy.onEnergyExhausted.AddListener(DisableMovement);
            _parkingSpot.onAllScootersParked.AddListener(DisableMovement);
        }

        private void Update()
        {
            GetInput();
        }

        private void FixedUpdate()
        {
            if (_inputValue == Vector3.zero)
            {
                _rb.velocity = Vector3.zero;
            }

            Move();
            LookForward();
            UpdateAnimation();
        }

        private void OnDisable()
        {
            _movementInput.Disable();

            _energy.onEnergyExhausted.RemoveListener(DisableMovement);
            _parkingSpot.onAllScootersParked.RemoveListener(DisableMovement);
        }

        public bool IsStill()
        {
            if (_rb == null) return false;

            if (_rb.velocity.sqrMagnitude <= _movementTolerance)
            {
                return true;
            }

            return false;
        }

        private void AddMoveSpeed()
        {
            if (_currentMovementEffect != null)
            {
                StopCoroutine(_currentMovementEffect);
            }
            _currentMovementEffect = StartCoroutine(GrantMoveSpeedEffect());
        }

        private IEnumerator GrantMoveSpeedEffect()
        {
            float durationRemaining = _buffDuration;

            _currentMaxSpeed = _initialMaxSpeed + _additionalSpeed;
            while (durationRemaining >= 0)
            {
                durationRemaining -= Time.deltaTime;
                yield return null;
            }
            _currentMaxSpeed = _initialMaxSpeed;
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
            if (playerVelocity.sqrMagnitude > _currentMaxSpeed * _currentMaxSpeed)
            {
                _rb.velocity = playerVelocity.normalized * _currentMaxSpeed + Vector3.up * _rb.velocity.y;
            }
        }

        private void LookForward()
        {
            Vector3 relativePosition = (transform.position - _inputValue) - transform.position;
            if (relativePosition == Vector3.zero) return;

            Quaternion lookRotation = Quaternion.LookRotation(-relativePosition, Vector3.up);
            transform.rotation = lookRotation;

        }

        private void UpdateAnimation()
        {
            Vector3 playerVelocity = _rb.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);
            float currentSpeed = localVelocity.z;

            _playerAnimator.SetFloat("forwardSpeed", currentSpeed);

        }
    }
}
