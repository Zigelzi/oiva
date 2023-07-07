using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_Energy_", menuName = "Status/Effects/Create Energy Effect", order = 2)]
    public class EnergyEffect : EffectStrategy
    {
        [SerializeField] float _amount = 0;

        public override void StartEffect(StatusData status)
        {

        }

        public override void ClearEffect(StatusData status)
        {

        }
    }
}
