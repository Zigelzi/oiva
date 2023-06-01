using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (InteractWithComponent()) return;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetTouchRay());

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
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            return ray;
        }
    }
}
