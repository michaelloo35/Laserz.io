using UnityEngine;

public class PlayerMovement
{
    private readonly int playerId;

    public PlayerMovement(int playerId)
    {
        this.playerId = playerId;
    }

    public void Move(Transform transform)
    {
        PlayerMoveInputs playerMoveInputs = new PlayerMoveInputs(playerId);

        Vector3 moveVector = new Vector3(playerMoveInputs.MoveXAxis, playerMoveInputs.MoveYAxis, 0.0f);
        transform.position += moveVector * Time.deltaTime;
    }
}