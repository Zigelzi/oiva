using Oiva.Discovery;
using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Control
{
    public class Carrying : MonoBehaviour
    {
        Scooter _currentScooter;

        public UnityEvent onScooterParked;
        public Scooter CurrentScooter { get { return _currentScooter; } }

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
        }

        private void Park(ParkingSpot parkingSpot)
        {
            if (_currentScooter == null) return;

            parkingSpot.Add(_currentScooter);
            _currentScooter.Park();
            _currentScooter = null;
            onScooterParked?.Invoke();
        }

    }
}
