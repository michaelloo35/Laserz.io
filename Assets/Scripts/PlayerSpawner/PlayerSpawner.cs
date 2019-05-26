using System.ComponentModel;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerController playerPrefab;
    public EnemyController enemyPrefab;
    private Transform[] players;
    private Transform[] enemies;
    private readonly int playerCount = 2;
    private readonly int enemyCount = 2;


    // Start is called before the first frame update
    void Start()
    {
        players = new Transform[playerCount];
        enemies = new Transform[enemyCount];
        SpawnPlayers(playerCount);
        SpawnEnemies(enemyCount);
        InitializeCameraManager();
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        for (int id = 0; id < numberOfPlayers; id++)
        {
            var player = Instantiate(playerPrefab, new Vector3(0, id), Quaternion.identity);
            player.Initialize(id);
            players[id] = player.transform;
        }
    }

    private void SpawnEnemies(int numberOfEnemies)
    {
        for (int id = 0; id < numberOfEnemies; id++)
        {
            var enemy = Instantiate(enemyPrefab, new Vector3(1, id), Quaternion.identity);
            enemy.Initialize(id);
            enemies[id] = enemy.transform;
        }
    }
    
    private void InitializeCameraManager()
    {
        GetComponent<CameraManager>().Initialize(players);
    }
    
}