using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour,Damagable
{
    public BoardManager boardManager;
    private readonly float MAX_HEALTH = 100;
    public float health = 100;
    public int id;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI killed;
    public int killCounter;
    public PlayerStatus playerStatus;
    public PlayerMovement playerMovement;
    public Weapon weapon;
    public GameObject deathAnimationPrefab;
    public Image healthBar;
    public DisplayDamage damageTextPrefab;

    public void Initialize(int playerId, BoardManager boardManager)
    {
        id = playerId;
        this.boardManager = boardManager;
        playerStatus = new PlayerStatus();
        playerName.SetText("PLAYER " + id);
        playerMovement = new PlayerMovement(playerId);
        weapon.Initialize(playerId, playerStatus);
        killed.SetText("");
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


    public void TakeDamage(float amount, GameObject attacker)
    {
        var damageTextPosition = new Vector3(transform.position.x, transform.position.y + 0.3f);
        var damageText = Instantiate(damageTextPrefab, damageTextPosition, Quaternion.identity);
        damageText.Initialize(amount);
        Destroy(damageText.gameObject, 0.5f);
        health -= amount;
        healthBar.fillAmount = health / MAX_HEALTH;
        if (health <= 0)
        {
            Die(attacker);
        }
    }

    public void Die(GameObject caller)
    {
        playerStatus.dead = true;
        GameObject deathEffect = Instantiate(deathAnimationPrefab,
            new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(deathEffect, 3.0f);
        Respawn();
        playerStatus.dead = false;

        if (caller.CompareTag("Player"))
        {
            caller.GetComponent<PlayerController>().UpdateKillCounter();
        }
    }

    public void UpdateKillCounter()
    {
        killed.SetText((++killCounter).ToString());
    }

    private void Respawn()
    {
        transform.position = boardManager.getSpawnPoint();
        health = MAX_HEALTH;
        healthBar.fillAmount = health / MAX_HEALTH;
    }
}