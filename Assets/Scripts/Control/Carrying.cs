using Oiva.Discovery;
using Oiva.Status;
using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Control
{
    public class Carrying : MonoBehaviour
    {
        [SerializeField] float _movementPenalty = 3f;
        [SerializeField] GameObject _carryVfx;

        ParticleSystem _vfxParticleSystem;
        Scooter _currentScooter;
        StatusManager _statusManager;

        public UnityEvent onScooterParked;
        public Scooter CurrentScooter { get { return _currentScooter; } }

        // Prototyping
        Movement _movement;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _statusManager = GetComponent<StatusManager>();

            if (_carryVfx == null) return;

            _vfxParticleSystem = _carryVfx.GetComponentInChildren<ParticleSystem>();
            _vfxParticleSystem.Stop();
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
            ToggleVFX();
            _statusManager.ApplyPickupStatuses();
        }

        private void Park(ParkingSpot parkingSpot)
        {
            if (_currentScooter == null) return;

            parkingSpot.Add(_currentScooter);
            _currentScooter.Park();
            _currentScooter = null;
            onScooterParked?.Invoke();
            ToggleVFX();
            _statusManager.ApplyParkingStatuses();
        }

        private void ToggleVFX()
        {
            if (_currentScooter)
            {
                _vfxParticleSystem.Play();
            }
            else
            {
                _vfxParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }

    }
}
