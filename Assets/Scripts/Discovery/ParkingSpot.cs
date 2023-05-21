using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Discovery
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
            _totalScooters += FindObjectsOfType<Hideout>().Length;

        }

        public void Add(Scooter scooter)
        {
            if (_parkStart == null) return;

            scooter.transform.SetParent(_parkStart);
            scooter.transform.position = _parkStart.position;
            scooter.transform.localPosition = _parkedScooters * Vector3.left;
            scooter.transform.rotation = _parkStart.rotation;

            _parkedScooters++;
            onSingleScooterParked?.Invoke();

            if (_parkedScooters >= _totalScooters)
            {
                onAllScootersParked?.Invoke();
            }
        }
    }

}