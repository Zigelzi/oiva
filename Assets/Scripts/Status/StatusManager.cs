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
                StatusData statusData = new StatusData(this.gameObject);
                status.Apply(statusData);
            }
        }

        public void ApplyParkingStatuses()
        {
            foreach (Status pickupStatus in _pickupStatuses)
            {
                StatusData statusData = new StatusData(this.gameObject);
                pickupStatus.Clear(statusData);
            }
            foreach (Status status in _parkStatuses)
            {
                StatusData statusData = new StatusData(this.gameObject);
                status.Apply(statusData);
            }

        }
    }
}
