using UnityEngine;
using UnityEngine.AI;

namespace Oiva.Commuter
{
    public class CommuterMovement : MonoBehaviour
    {
        [SerializeField] Transform _destination;
        NavMeshAgent _navAgent;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _destination = FindObjectOfType<Destination>().transform;

            if (_destination == null) return;
            if (_navAgent == null) return;

            _navAgent.SetDestination(_destination.position);
        }
    }
}
