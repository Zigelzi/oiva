using Oiva.Discovery;
using Oiva.Status;
using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Control
{
    public class Carrying : MonoBehaviour
    {
        [SerializeField] float _movementPenalty = 3f;

        Scooter _currentScooter;
        StatusManager _statusManager;

        public UnityEvent onScooterParked;
        public Scooter CurrentScooter { get { return _currentScooter; } }


        private void Awake()
        {
            _statusManager = GetComponent<StatusManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ParkingSpot>(out ParkingSpot parkingSpot))
            {
                Park(parkingSpot);
            }
        }

        public void PickUp(Scooter newScooter)
        {
            if (_currentScooter != null) return;
            _currentScooter = newScooter;
            _statusManager.ApplyPickupStatuses();
        }

        private void Park(ParkingSpot parkingSpot)
        {
            if (_currentScooter == null) return;

            parkingSpot.Add(_currentScooter);
            _currentScooter.Park();
            _currentScooter = null;
            onScooterParked?.Invoke();
            _statusManager.ApplyParkingStatuses();
        }

    }
}
