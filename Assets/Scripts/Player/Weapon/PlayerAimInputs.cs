using UnityEngine;


public struct PlayerAimInputs
{
    public float AimXAxis;
    public float AimYAxis;

    public PlayerAimInputs(int playerId)
    {
        AimXAxis = Input.GetAxis($"Aim_Horizontal_{playerId}");
        AimYAxis = Input.GetAxis($"Aim_Vertical_{playerId}");
    }
}