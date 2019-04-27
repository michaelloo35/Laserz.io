using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private int playerId;
    public PlayerStatus playerStatus;
    public PlayerMovement playerMovement;
    public Weapon weapon;
    public GameObject deathAnimationPrefab;


    public void Initialize(int playerId)
    {
        this.playerId = playerId;
        playerMovement = new PlayerMovement(playerId);
        playerStatus = new PlayerStatus();
        weapon.Initialize(playerId, playerStatus);
    }

    public void Update()
    {
        if (!playerStatus.blocked)
        {
            playerMovement.Move(transform);
            weapon.Aim();
        }

        weapon.Shoot();
    }


    public void Die()
    {
        playerStatus.dead = true;
        GameObject deathEffect = Instantiate(deathAnimationPrefab,
            new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(deathEffect, 3.0f);
        Respawn();
        playerStatus.dead = false;
    }

    private void Respawn()
    {
        var random = new Random();
        transform.position = new Vector3(
            random.Next() * (float) random.NextDouble() % 1,
            random.Next() * (float) random.NextDouble() % 1);
    }
}