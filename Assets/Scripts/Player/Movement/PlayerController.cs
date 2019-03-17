﻿using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("playerName")] public int playerId;

    // Update is called once per frame
    void Update()
    {
        PlayerMoveInputs playerMoveInputs = new PlayerMoveInputs(playerId);
        MoveXY(playerMoveInputs);
    }

    private void MoveXY(PlayerMoveInputs playerMoveInputs)
    {
        Vector3 moveVector = new Vector3(playerMoveInputs.MoveXAxis, playerMoveInputs.MoveYAxis, 0.0f);
        transform.position += moveVector * Time.deltaTime;
    }
}