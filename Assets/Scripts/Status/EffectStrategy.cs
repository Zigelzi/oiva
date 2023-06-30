using UnityEngine;

namespace Oiva.Status
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartEffect(GameObject user);

        public abstract void ClearEffect(GameObject user);
    }
}
