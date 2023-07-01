using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Status", menuName = "Status/Create New Effect", order = 0)]
    public class Status : ScriptableObject
    {
        [SerializeField] EffectStrategy[] _effectStrategies;

        public void Apply(StatusData status)
        {
            foreach (EffectStrategy effect in _effectStrategies)
            {
                effect.StartEffect(status);
            }
        }

        public void Clear(StatusData status)
        {
            foreach (EffectStrategy effect in _effectStrategies)
            {
                effect.ClearEffect(status);
            }
        }
    }

}