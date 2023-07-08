using Oiva.Control;
using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_Energy_", menuName = "Status/Effects/Create Energy Effect", order = 2)]
    public class EnergyEffect : EffectStrategy
    {
        [SerializeField][Range(0, 20)] float _amount = 0;

        public override void StartEffect(StatusData status)
        {
            Energy energy = status.Owner.GetComponent<Energy>();

            if (energy == null) return;

            if (status.Duration > 0)
            {
                energy.StopEnergyConsumptionTemporarily(status.Duration);

            }
            else
            {
                energy.ChangeEnergyConsumptionPermanently(_amount);
            }
        }

        public override void ClearEffect(StatusData status)
        {
            Energy energy = status.Owner.GetComponent<Energy>();

            if (energy == null) return;

            energy.RestoreDefaultEnergyConsumption();
        }
    }
}
