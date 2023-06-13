using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Oiva.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _interactionRadius = .5f;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        private void Update()
        {
            if (InteractWithComponent()) return;
        }

        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        private bool InteractWithComponent()
        {
            if (Touch.activeTouches.Count == 0) return false;

            RaycastHit[] hits = Physics.SphereCastAll(GetTouchRay(), _interactionRadius);

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static Ray GetTouchRay()
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (Touch.activeTouches.Count > 1)
            {
                touchPosition = Touch.activeTouches[Touch.activeTouches.Count - 1].screenPosition;
            }

            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            return ray;
        }
    }
}
