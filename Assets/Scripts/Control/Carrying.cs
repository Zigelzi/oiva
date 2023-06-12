using Oiva.Discovery;
using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Control
{
    public class Carrying : MonoBehaviour
    {
        [SerializeField] float _movementPenalty = 3f;
        Scooter _currentScooter;

        public UnityEvent onScooterParked;
        public Scooter CurrentScooter { get { return _currentScooter; } }

        // Prototyping
        Movement _movement;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
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
            _movement.ReduceMovementSpeed(_movementPenalty);
        }

        private void Park(ParkingSpot parkingSpot)
        {
            if (_currentScooter == null) return;

            parkingSpot.Add(_currentScooter);
            _currentScooter.Park();
            _currentScooter = null;
            _movement.RestoreDefaultMovementSpeed();
            onScooterParked?.Invoke();
        }

    }
}
