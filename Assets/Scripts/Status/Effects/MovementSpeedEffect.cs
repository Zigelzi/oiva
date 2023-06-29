using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_MovementSpeed", menuName = "Status/Effects/Create Movement Speed Effect", order = 0)]
    public class MovementSpeedEffect : EffectStrategy
    {
        [SerializeField] float _movementSpeedChange = 5f;
        public override void StartEffect()
        {
            Debug.Log("Starting movement speed effect!");
        }
    }
}
