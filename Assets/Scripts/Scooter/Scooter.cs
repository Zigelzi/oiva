using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oiva.Scooter
{
    public class Scooter : MonoBehaviour
    {
        [SerializeField] Vector3 _followOffset;
        Transform _owner;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                PickUp(other.gameObject.transform);
            }
        }

        private void Update()
        {
            Follow();
        }

        private void PickUp(Transform newOwner)
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