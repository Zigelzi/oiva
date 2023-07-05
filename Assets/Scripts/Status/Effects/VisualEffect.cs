using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_Visual", menuName = "Status/Effects/Create Visual Effect", order = 1)]
    public class VisualEffect : EffectStrategy
    {
        [SerializeField] GameObject vfxPrefab;
        public override void StartEffect(StatusData status)
        {
            Instantiate(vfxPrefab, status.HeadCastPoint);
        }

        public override void ClearEffect(StatusData status)
        {
            DestroyFXAfterEffect[] visualEffects = status.HeadCastPoint.GetComponentsInChildren<DestroyFXAfterEffect>();

            foreach (DestroyFXAfterEffect vfx in visualEffects)
            {
                vfx.DestroyEffect(0);
            }
        }
    }
}
