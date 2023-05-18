using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Scooter
{
    public class ParkingSpot : MonoBehaviour
    {
        [SerializeField] Transform _parkStart;

        int _totalScooters = 0;
        int _parkedScooters = 0;

        public UnityEvent onSingleScooterParked;
        public UnityEvent onAllScootersParked;

        private void Awake()
        {
            _totalScooters = FindObjectsOfType<Scooter>().Length;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Scooter>(out Scooter scooter))
            {
                Park(scooter);
            }
        }

        private void Park(Scooter scooter)
        {
            if (_parkStart == null) return;

            scooter.Park();
            scooter.transform.SetParent(_parkStart);
            scooter.transform.position = _parkStart.position;
            scooter.transform.rotation = _parkStart.rotation;

            _parkedScooters++;
            onSingleScooterParked?.Invoke();

            if (_parkedScooters >= _totalScooters)
            {
                onAllScootersParked?.Invoke();
                Debug.Log("All scooters parked!");
            }
        }
    }

}