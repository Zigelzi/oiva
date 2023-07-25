using UnityEngine;

namespace Oiva.Commuter
{
    public class CommuterSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _commuterPrefab;

        private void Awake()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (_commuterPrefab == null) return;

            Instantiate(_commuterPrefab, transform.position, Quaternion.identity);
        }
    }
}
