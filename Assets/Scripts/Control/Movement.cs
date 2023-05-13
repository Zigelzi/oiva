using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Oiva.Control
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        PlayerInputActions _playerInputActions;
        InputAction _movementInput;
        Rigidbody _rb;

        Vector3 _inputValue = new Vector3();

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

            Vector3 newPosition = transform.position + _inputValue * _speed * Time.deltaTime;
            _rb.MovePosition(newPosition);
        }

        private void LookForward()
        {
            Vector3 relativePosition = (transform.position - _inputValue) - transform.position;
            if (relativePosition == Vector3.zero) return;

            Quaternion lookRotation = Quaternion.LookRotation(-relativePosition, Vector3.up);
            transform.rotation = lookRotation;
            
        }



    }
}
