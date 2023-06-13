using UnityEngine;
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

            Touch.onFingerDown += InteractWithComponent;
        }

        private void OnDisable()
        {
            Touch.onFingerDown -= InteractWithComponent;

            EnhancedTouchSupport.Disable();
        }

        private void InteractWithComponent(Finger finger)
        {

            RaycastHit[] hits = Physics.SphereCastAll(GetTouchRay(finger.currentTouch.screenPosition), _interactionRadius);

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        return;
                    }
                }
            }

            return;
        }

        private static Ray GetTouchRay(Vector2 position)
        {

            Ray ray = Camera.main.ScreenPointToRay(position);

            return ray;
        }
    }
}
