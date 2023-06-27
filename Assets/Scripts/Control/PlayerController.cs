using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _interactionRadius = 2f;

        PlayerInputActions _playerInputActions;
        InputAction _interactAction;

        private void Awake()
        {
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
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _interactionRadius, Vector3.up, _interactionRadius);
            float[] distanceFromHit = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                distanceFromHit[i] = hits[i].distance;
            }



            RaycastHit[] sortedHits = hits;
            Debug.Log("Before sort:");
            foreach (RaycastHit hit in sortedHits)
            {
                Debug.Log($"{hit.collider.transform.name}");
            }
            Array.Sort(distanceFromHit, sortedHits);

            Debug.Log("After sort:");

            foreach (RaycastHit hit in sortedHits)
            {
                Debug.Log($"{hit.collider.transform.name}");
            }
            foreach (RaycastHit hit in sortedHits)
            {
                IRaycastable[] interactables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable interactable in interactables)
                {
                    interactable.HandleRaycast(this);
                }
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
