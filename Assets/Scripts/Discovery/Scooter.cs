using Oiva.Control;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Scooter : MonoBehaviour, IRaycastable
    {
        [SerializeField] Vector3 _followOffset;
        [SerializeField] float _maxPickupDistance = 2f;
        Transform _owner;
        bool _isParked = false;

        public bool IsParked { get { return _isParked; } }

        private void Update()
        {
            if (!_isParked)
            {
                Follow();
            }
        }

        public void SetOwner(Transform newOwner)
        {
            _owner = newOwner;
        }

        public void Park()
        {
            _isParked = true;
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
            Carrying carrying = playerController.transform.GetComponent<Carrying>();
            Vector3 playerVelocity = playerController.transform.GetComponent<Rigidbody>().velocity;
            bool isStill = Mathf.Approximately(playerVelocity.sqrMagnitude, 0);

            if (distanceToPlayer <= _maxPickupDistance &&
                isStill &&
                carrying.CurrentScooter == null &&
                !_isParked)
            {
                _owner = playerController.transform;
                carrying.PickUp(this);
                return true;
            }
            return false;
        }

        private void Follow()
        {
            if (_owner == null) return;

            transform.position = _owner.position + _followOffset;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 cubeSize = new Vector3(_maxPickupDistance, _maxPickupDistance, _maxPickupDistance);
            Gizmos.DrawWireSphere(transform.position, _maxPickupDistance);
        }
    }

}