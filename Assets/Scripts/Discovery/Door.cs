using Oiva.Control;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Door : MonoBehaviour, IRaycastable
    {
        private Animator _animator;
        private bool _isOpen = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            if (_isOpen)
            {
                _animator.Play("Door_Close");
            }
            else
            {
                _animator.Play("Door_Open");
            }
            _isOpen = !_isOpen;

            return true;
        }
    }
}
