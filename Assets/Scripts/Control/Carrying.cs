using Oiva.Discovery;
using UnityEngine;

namespace Oiva.Control
{
    public class Carrying : MonoBehaviour
    {
        Scooter _currentScooter;

        public Scooter CurrentScooter { get { return _currentScooter; } }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Scooter>(out Scooter scooter))
            {
                if (scooter.IsParked) return;
                PickUp(scooter);
            }

            if (other.TryGetComponent<ParkingSpot>(out ParkingSpot parkingSpot))
            {
                Park(parkingSpot);
            }
        }
        private void PickUp(Scooter newScooter)
        {
            if (_currentScooter != null) return;
            _currentScooter = newScooter;
            newScooter.SetOwner(transform);
        }

        private void Park(ParkingSpot parkingSpot)
        {
            if (_currentScooter == null) return;

            parkingSpot.Add(_currentScooter);
            _currentScooter.Park();
            _currentScooter = null;
        }

    }
}
