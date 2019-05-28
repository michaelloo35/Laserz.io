using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemyPrefab;
    public BoardManager boardManager;
    public float spawningInterval;
    public int enemiesPerSpawn;
    private float timeToNextSpawn;
    private int currentId;

    private void Update()
    {
        if (Time.time > timeToNextSpawn)
        {
            SpawnEnemies();
            timeToNextSpawn = Time.time + spawningInterval;
        }
    }


    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            var enemy = Instantiate(enemyPrefab, boardManager.getSpawnPoint(), Quaternion.identity);
            enemy.Initialize(++currentId);
        }
    }
}