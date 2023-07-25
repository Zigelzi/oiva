using UnityEngine;

namespace Oiva.Commuter
{
    public class Destination : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CommuterMovement>(out CommuterMovement commuter))
            {
                Destroy(commuter.gameObject);
            }
        }
    }
}
