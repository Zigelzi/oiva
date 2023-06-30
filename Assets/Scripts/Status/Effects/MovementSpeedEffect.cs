using Oiva.Control;
using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_MovementSpeed", menuName = "Status/Effects/Create Movement Speed Effect", order = 0)]
    public class MovementSpeedEffect : EffectStrategy
    {
        [SerializeField] float _movementSpeedChange = 5f;
        public override void StartEffect(GameObject user)
        {
            if (user.TryGetComponent<Movement>(out Movement movement))
            {
                movement.ReduceMovementSpeed(_movementSpeedChange);
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
