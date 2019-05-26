using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public int id;
    public EnemyStatus enemyStatus;

    public float baseSpeed = 0.8f;
    public float stoppingDistance;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(15, gameObject);
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

    private float speedFunction(float distance)
    {
        return (float) (1 / (distance + baseSpeed) + 0.1);
    }
}