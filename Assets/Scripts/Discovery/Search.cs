using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Discovery
{
    public class Search : MonoBehaviour
    {
        [SerializeField] float _interactionTolerance = 1f;
        [SerializeField] float _maxInteractionDistance = 3.2f;
        [SerializeField] LayerMask _interactableLayers;
        Camera _mainCamera;
        Rigidbody _rb;
        private void Awake()
        {
            _mainCamera = Camera.main;
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            bool isStill = Mathf.Approximately(_rb.velocity.sqrMagnitude, 0);

            if (!Touchscreen.current.primaryTouch.press.isPressed) return;
            if (!isStill) return;

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
                if (hit.collider.TryGetComponent<Hideout>(out Hideout hideout))
                {
                    hideout.SpawnScooter();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 cubeSize = new Vector3(_maxInteractionDistance, _maxInteractionDistance, _maxInteractionDistance);
            Gizmos.DrawCube(transform.position, cubeSize);
        }
    }
}
