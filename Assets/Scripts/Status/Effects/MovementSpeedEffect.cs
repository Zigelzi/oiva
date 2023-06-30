using Oiva.Control;
using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_MovementSpeed", menuName = "Status/Effects/Create Movement Speed Effect", order = 0)]
    public class MovementSpeedEffect : EffectStrategy
    {
        [SerializeField] float _movementSpeedChange = 5f;
        [SerializeField] float _duration = 0;

        public override void StartEffect(GameObject user)
        {
            Movement movement = user.GetComponent<Movement>();
            if (movement == null) return;

            if (_duration > 0)
            {
                movement.ChangeMovementSpeedTemporarily(_movementSpeedChange, _duration);
            }
            else
            {
                movement.ChangeMovementSpeedPermanently(_movementSpeedChange);
            }
        }

        public override void ClearEffect(GameObject user)
        {
            if (user.TryGetComponent<Movement>(out Movement movement))
            {
                movement.RestoreDefaultMovementSpeed();
            }
        }
    }
}
