using UnityEngine;

namespace Oiva.Status
{
    [CreateAssetMenu(fileName = "Effect_Visual", menuName = "Status/Effects/Create Visual Effect", order = 1)]
    public class VisualEffect : EffectStrategy
    {
        [SerializeField] GameObject vfxPrefab;
        [SerializeField] bool isSpawnedAtHead = false;
        public override void StartEffect(StatusData status)
        {
            if (status.Duration == 0)
            {
                SpawnEffect(status);
            }
            else
            {
                SpawnTemporaryEffect(status);
            }
        }

        public override void ClearEffect(StatusData status)
        {
            DestroyFXAfterEffect[] visualEffects = status.HeadCastPoint.GetComponentsInChildren<DestroyFXAfterEffect>();

            foreach (DestroyFXAfterEffect vfx in visualEffects)
            {
                vfx.DestroyEffect(0);
            }
        }

        private GameObject SpawnEffect(StatusData status)
        {
            if (isSpawnedAtHead)
            {
                return Instantiate(vfxPrefab, status.HeadCastPoint);
            }
            else
            {
                return Instantiate(vfxPrefab, status.FeetCastPoint);
            }
        }

        private void SpawnTemporaryEffect(StatusData status)
        {
            StatusManager statusManager = status.Owner.GetComponent<StatusManager>();
            if (statusManager.CurrentStatusVfx != null)
            {
                Destroy(statusManager.CurrentStatusVfx);
            }

            statusManager.CurrentStatusVfx = SpawnEffect(status);
            Destroy(statusManager.CurrentStatusVfx, status.Duration);
        }
    }
}
