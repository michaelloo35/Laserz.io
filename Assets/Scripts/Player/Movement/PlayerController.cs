using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("playerName")] public int playerId;

    [FormerlySerializedAs("PlayerStatus")] public PlayerStatus playerStatus;
    // Update is called once per frame
    void Update()
    {
        if (!playerStatus.blocked)
        {
            PlayerMoveInputs playerMoveInputs = new PlayerMoveInputs(playerId);
            MoveXY(playerMoveInputs);
        }
    }

    private void MoveXY(PlayerMoveInputs playerMoveInputs)
    {
        Vector3 moveVector = new Vector3(playerMoveInputs.MoveXAxis, playerMoveInputs.MoveYAxis, 0.0f);
        transform.position += moveVector * Time.deltaTime;
    }
}