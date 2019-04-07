using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public int playerId;
    public PlayerStatus playerStatus;
    public GameObject deathEffectTemplate;

    public
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

    public void Die()
    {
        playerStatus.dead = true;
        GameObject deathEffect = Instantiate(deathEffectTemplate,
            new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(deathEffect, 3.0f);
        Respawn();
        playerStatus.dead = false;
    }

    private void Respawn()
    {
        transform.position = new Vector3(new Random().Next() % 2, new Random().Next() % 2);
    }
}