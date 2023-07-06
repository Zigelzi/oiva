using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Status", menuName = "Status/Create New Effect", order = 0)]
    public class Status : ScriptableObject
    {
        [SerializeField] float _duration = 0;
        [SerializeField] EffectStrategy[] _effectStrategies;

        public void Apply(GameObject owner)
        {
            StatusData status = new StatusData(owner, _duration);
            foreach (EffectStrategy effect in _effectStrategies)
            {
                effect.StartEffect(status);
            }
        }

        public void Clear(GameObject owner)
        {
            StatusData status = new StatusData(owner, _duration);
            foreach (EffectStrategy effect in _effectStrategies)
            {
                effect.ClearEffect(status);
            }
        }
    }

}