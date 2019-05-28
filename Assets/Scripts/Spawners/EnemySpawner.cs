using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemyPrefab;
    public BoardManager boardManager;
    public float spawningInterval;
    public int enemiesPerSpawn;
    private float timeToNextSpawn;
    private List<Transform> enemies = new List<Transform>();


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
        for (int id = 0; id < enemiesPerSpawn; id++)
        {
            var enemy = Instantiate(enemyPrefab, boardManager.getSpawnPoint(), Quaternion.identity);
            enemy.Initialize(id);
            enemies.Add(enemy.transform);
        }
    }
}