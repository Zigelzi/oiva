using Oiva.Control;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oiva.Discovery
{
    public class Hideout : MonoBehaviour, IRaycastable
    {
        [SerializeField] GameObject _scooterPrefab;
        [SerializeField] Transform _spawnPosition;
        [SerializeField] float _maxInteractionDistance = 3.2f;

        bool _hasSpawnedScooter = false;

        public void SpawnScooter()
        {
            if (_scooterPrefab == null || _spawnPosition == null) return;

            if (_hasSpawnedScooter) return;

            Instantiate(_scooterPrefab, _spawnPosition.position, Quaternion.identity);
            _hasSpawnedScooter = true;
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);

            if (distanceToPlayer <= _maxInteractionDistance &&
                Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                Debug.Log("Interacted with hideout!");
                return true;
            }
            return false;
        }
    }
}
