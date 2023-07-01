using UnityEngine;

namespace Oiva.Status
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartEffect(StatusData status);

        public abstract void ClearEffect(StatusData status);
    }
}
