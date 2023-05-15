using UnityEngine;

namespace Oiva.Scooter
{
    public class ParkingSpot : MonoBehaviour
    {
        [SerializeField] Transform _parkStart;
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
        }
    }

}