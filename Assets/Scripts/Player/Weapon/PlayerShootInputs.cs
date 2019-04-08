using UnityEngine;

public struct PlayerShootInputs
{
    public float shoot;

    public PlayerShootInputs(int playerId)
    {
        shoot = Input.GetAxisRaw($"Fire_{playerId}");
    }
}