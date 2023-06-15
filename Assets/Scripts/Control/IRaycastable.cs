using Oiva.Control;
using UnityEngine;

public interface IRaycastable
{
    Transform transform { get; }
    public bool HandleRaycast(PlayerController playerController);
}