using UnityEngine;

namespace Oiva.Discovery
{
    public class Hideout : MonoBehaviour
    {
        [SerializeField] GameObject _scooterPrefab;
        [SerializeField] Transform _spawnPosition;

        bool _hasSpawnedScooter = false;

        public void SpawnScooter()
        {
            if (_scooterPrefab == null || _spawnPosition == null) return;

            if (_hasSpawnedScooter) return;

            Instantiate(_scooterPrefab, _spawnPosition.position, Quaternion.identity);
            _hasSpawnedScooter = true;
        }
    }
}
