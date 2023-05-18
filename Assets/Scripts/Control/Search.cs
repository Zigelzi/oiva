using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Control
{
    public class Search : MonoBehaviour
    {
        [SerializeField] float _interactionTolerance = 1f;
        [SerializeField] float _maxInteractionDistance = 3.2f;
        [SerializeField] LayerMask _interactableLayers;
        Camera _mainCamera;
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!Touchscreen.current.primaryTouch.press.isPressed) return;
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

            if (Physics.SphereCast(ray,
                _interactionTolerance,
                out RaycastHit hit,
                Mathf.Infinity,
                _interactableLayers))
            {
                TryInteractWithHideout(hit);
            }

        }

        private void TryInteractWithHideout(RaycastHit hit)
        {
            float distanceToHideout = Vector3.Distance(transform.position, hit.collider.transform.position);
            if (distanceToHideout <= _maxInteractionDistance)
            {
                Debug.Log($"Interacting with {hit.collider.gameObject.name}!");
            }
        }
    }
}
