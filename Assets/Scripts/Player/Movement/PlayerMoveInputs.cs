using UnityEngine;


public struct PlayerMoveInputs
{
    public float MoveXAxis;
    public float MoveYAxis;

    public PlayerMoveInputs(int playerId)
    {
        MoveXAxis = Input.GetAxis($"Horizontal_{playerId}");
        MoveYAxis = Input.GetAxis($"Vertical_{playerId}");
    }
}