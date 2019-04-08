using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public int playerId;
    public PlayerStatus playerStatus;
    public PlayerMovement playerMovement;
    public Weapon weapon;


    private void Start()
    {
        playerMovement = new PlayerMovement(playerId);
        weapon =
            new GameObject("Laser")
                .AddComponent<Laser>()
                .Initialize(gameObject, playerId, playerStatus);
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
        GameObject deathEffect = Instantiate(GlobalObjects.deathAnimation,
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