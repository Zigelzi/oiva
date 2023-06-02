using Oiva.Control;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Hideout : MonoBehaviour, IRaycastable
    {
        [SerializeField] GameObject _scooterPrefab;
        [SerializeField] Transform _spawnPosition;
        [SerializeField] float _maxInteractionDistance = 3.2f;

        bool _hasSpawnedScooter = false;

        private void SpawnScooter()
        {
            if (_scooterPrefab == null || _spawnPosition == null) return;

            if (_hasSpawnedScooter) return;

            Instantiate(_scooterPrefab, _spawnPosition.position, Quaternion.identity);
            _hasSpawnedScooter = true;
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            float distanceToPlayer = Vector3.Distance(playerController.transform.position, transform.position);
            Vector3 playerVelocity = playerController.transform.GetComponent<Rigidbody>().velocity;

            bool isStill = Mathf.Approximately(playerVelocity.sqrMagnitude, 0);
            if (distanceToPlayer <= _maxInteractionDistance &&
                isStill &&
                !_hasSpawnedScooter)
            {
                SpawnScooter();
                return true;
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 cubeSize = new Vector3(_maxInteractionDistance, _maxInteractionDistance, _maxInteractionDistance);
            Gizmos.DrawWireCube(transform.position, cubeSize);
        }
    }
}
