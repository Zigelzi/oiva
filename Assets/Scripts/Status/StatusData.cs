using UnityEngine;

namespace Oiva.Status
{
    public class StatusData
    {
        GameObject _owner;
        Transform _feetSpawnpoint;
        Transform _headSpawnpoint;

        public GameObject Owner { get { return _owner; } }
        public Transform FeetCastPoint { get { return _feetSpawnpoint; } }
        public Transform HeadCastPoint { get { return _headSpawnpoint; } }
        public StatusData(GameObject newOwner)
        {
            this._owner = newOwner;
            this._feetSpawnpoint = newOwner.GetComponentInChildren<SpawnpointFeet>().transform;
            this._headSpawnpoint = newOwner.GetComponentInChildren<SpawnpointHead>().transform;
        }

    }
}
