using UnityEngine;

namespace Oiva.Status
{
    public class StatusManager : MonoBehaviour
    {
        [SerializeField] Status[] _pickupStatuses;
        [SerializeField] Status[] _parkStatuses;
        public void ApplyPickupStatuses()
        {
            foreach (Status status in _pickupStatuses)
            {
                status.Apply(gameObject);
            }
        }

        public void ApplyParkingStatuses()
        {
            foreach (Status pickupStatus in _pickupStatuses)
            {
                pickupStatus.Clear(gameObject);
            }
            foreach (Status status in _parkStatuses)
            {
                status.Apply(gameObject);
            }

        }
    }
}
