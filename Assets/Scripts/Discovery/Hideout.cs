using Oiva.Control;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Hideout : MonoBehaviour, IRaycastable
    {
        [SerializeField] GameObject _scooterPrefab;
        [SerializeField] Transform _spawnPosition;

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
            bool isStill = playerController.GetComponent<Movement>().IsStill();
            if (isStill &&
                !_hasSpawnedScooter)
            {
                SpawnScooter();
                return true;
            }
            return false;
        }
    }
}
