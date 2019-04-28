using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private readonly float MAX_HEALTH = 100;
    public int id;
    public TextMeshProUGUI playerName;
    public float health = 100;
    public PlayerStatus playerStatus;
    public PlayerMovement playerMovement;
    public Weapon weapon;
    public GameObject deathAnimationPrefab;
    public Image healthBar;
    public DisplayDamage damageTextPrefab;

    public void Initialize(int playerId)
    {
        id = playerId;
        playerName.SetText("PLAYER " + id);
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


    public void TakeDamage(float amount)
    {
        var damageTextPosition = new Vector3(transform.position.x, transform.position.y + 0.3f);
        var damageText = Instantiate(damageTextPrefab, damageTextPosition, Quaternion.identity);
        damageText.Initialize(amount);
        Destroy(damageText.gameObject,0.5f);
        health -= amount;
        healthBar.fillAmount = health / MAX_HEALTH;
        if (health <= 0)
        {
            Die();
        }
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
        health = MAX_HEALTH;
        healthBar.fillAmount = health / MAX_HEALTH;
    }
}