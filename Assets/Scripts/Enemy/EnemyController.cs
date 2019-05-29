using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour, Damagable
{
    public int id;
    public EnemyStatus enemyStatus;

    private readonly float MAX_HEALTH = 100;
    public float health = 100;

    public float baseSpeed = 0.8f;
    public float stoppingDistance;
    public float attackSpeed = 1.0f;
    private float timeToNextAttack;

    public GameObject deathAnimationPrefab;

    public Image healthBar;
    public DisplayDamage damageTextPrefab;
    public Transform player;

    public void Initialize(int enemyId)
    {
        id = enemyId;
        enemyStatus = new EnemyStatus();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindClosestPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindClosestPlayer().transform;
        float distance = Vector2.Distance(transform.position, player.position);

        float speed = speedFunction(distance);
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }


    private void OnCollisionStay2D(Collision2D other)
    {

        if (Time.time > timeToNextAttack)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage(15, gameObject);
                timeToNextAttack = Time.time + 1.0f / attackSpeed;
            }
        }
    }


    public GameObject FindClosestPlayer()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gameObjects)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
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
        GameObject deathEffect = Instantiate(deathAnimationPrefab,
            new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(deathEffect, 3.0f);
        Destroy(gameObject);
    }

    private float speedFunction(float distance)
    {
        return (float) (1 / (distance + baseSpeed) + 0.1);
    }
}