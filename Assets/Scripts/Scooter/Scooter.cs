using UnityEngine;

namespace Oiva.Scooter
{
    public class Scooter : MonoBehaviour
    {
        [SerializeField] Vector3 _followOffset;
        Transform _owner;
        bool _isParked = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

                AssignOwner(other.gameObject.transform);
            }
        }

        private void Update()
        {
            if (!_isParked)
            {
                Follow();
            }
        }

        public void Park()
        {
            _owner = null;
            _isParked = true;
        }

        private void AssignOwner(Transform newOwner)
        {
            _owner = newOwner;
        }

        private void Follow()
        {
            if (_owner == null) return;

            transform.position = _owner.position + _followOffset;
        }
    }

}