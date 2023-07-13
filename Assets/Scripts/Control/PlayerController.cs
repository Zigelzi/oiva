using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _interactionRadius = 2f;

        Carrying _carrying;
        PlayerInputActions _playerInputActions;
        InputAction _interactAction;

        private void Awake()
        {
            _carrying = GetComponent<Carrying>();
            _playerInputActions = new PlayerInputActions();
        }
        private void OnEnable()
        {
            _interactAction = _playerInputActions.Player.Interact;

            _interactAction.Enable();

            _interactAction.performed += TryInteract;
        }

        private void OnDisable()
        {
            _interactAction.performed -= TryInteract;

            _interactAction.Disable();
        }

        private void TryInteract(InputAction.CallbackContext context)
        {
            if (_carrying.TryRelease()) return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRadius);
            float[] distanceFromCollider = new float[colliders.Length];

            for (int i = 0; i < colliders.Length; i++)
            {
                distanceFromCollider[i] = Vector3.Distance(transform.position, colliders[i].transform.position);
            }

            Collider[] sortedColliders = colliders;
            Array.Sort(distanceFromCollider, sortedColliders);

            foreach (Collider collider in sortedColliders)
            {
                IRaycastable interactable = collider.GetComponent<IRaycastable>();
                if (interactable == null) continue;

                // Only interact with the closest interactable and that can be interacted with.
                if (interactable != null && interactable.HandleRaycast(this)) break;
            }

        }

        private static Ray GetTouchRay(Vector2 position)
        {

            Ray ray = Camera.main.ScreenPointToRay(position);

            return ray;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _interactionRadius);
        }
    }
}
