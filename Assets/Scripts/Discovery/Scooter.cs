using Oiva.Control;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Scooter : MonoBehaviour, IRaycastable
    {
        [SerializeField] Vector3 _followOffset;

        [SerializeField]
        [Range(1, 100)] int _bounceProbability = 20;

        [SerializeField]
        [Range(30, 300)] float _minVerticalForce = 30;

        [SerializeField]
        [Range(100, 300)] float _maxVerticalForce = 80f;

        [SerializeField]
        [Range(30, 300)] float _minHorizontalForce = 10f;

        [SerializeField]
        [Range(100, 300)] float _maxHorizontalForce = 30f;

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
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Blinker>().StopBlinking();
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            Carrying carrying = playerController.transform.GetComponent<Carrying>();

            if (!IsInteractable(playerController)) return false;

            if (!TryBounce())
            {
                _owner = playerController.transform;
                carrying.PickUp(this);
            }

            return true;
        }

        private bool IsInteractable(PlayerController playerController)
        {
            Carrying carrying = playerController.transform.GetComponent<Carrying>();

            if (carrying.CurrentScooter == null &&
                !_isParked)
            {
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

        private bool TryBounce()
        {
            int currentBounceAttempt = Random.Range(1, 101);
            Rigidbody rb = GetComponent<Rigidbody>();
            float xForceAmount = Random.Range(-_minHorizontalForce, _maxHorizontalForce);
            float zForceAmount = Random.Range(-_minHorizontalForce, _maxHorizontalForce);
            float yForceAmount = Random.Range(_minVerticalForce, _maxVerticalForce);
            if (currentBounceAttempt <= _bounceProbability)
            {
                rb.AddForce(xForceAmount, yForceAmount, zForceAmount, ForceMode.Impulse);
                return true;
            }
            return false;
        }
    }

}