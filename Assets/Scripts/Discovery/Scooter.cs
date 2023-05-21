using UnityEngine;

namespace Oiva.Discovery
{
    public class Scooter : MonoBehaviour
    {
        [SerializeField] Vector3 _followOffset;
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

        private void Follow()
        {
            if (_owner == null) return;

            transform.position = _owner.position + _followOffset;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}