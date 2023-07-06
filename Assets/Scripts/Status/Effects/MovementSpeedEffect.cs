using Oiva.Control;
using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_MovementSpeed", menuName = "Status/Effects/Create Movement Speed Effect", order = 0)]
    public class MovementSpeedEffect : EffectStrategy
    {
        [SerializeField] float _movementSpeedChange = 5f;

        public override void StartEffect(StatusData status)
        {
            Movement movement = status.Owner.GetComponent<Movement>();
            if (movement == null) return;

            if (status.Duration > 0)
            {
                movement.ChangeMovementSpeedTemporarily(_movementSpeedChange, status.Duration);
            }
            else
            {
                movement.ChangeMovementSpeedPermanently(_movementSpeedChange);
            }
        }

        public override void ClearEffect(StatusData status)
        {
            if (status.Owner.TryGetComponent<Movement>(out Movement movement))
            {
                movement.RestoreDefaultMovementSpeed();
            }
        }
    }
}
