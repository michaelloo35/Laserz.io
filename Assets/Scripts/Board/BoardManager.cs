using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;

public class BoardManager : MonoBehaviour
{
    public int columns;

    public int rows;

    private float tileSize = 0.32f;

    public GameObject[] floorTiles;

    public GameObject wallTile;

    private Transform boardHolder;

    public GameObject spawnerPrefab;
    public List<GameObject> spawners = new List<GameObject>();
    public float spawnersR;

    public Vector2 downLeftCorner;
    public Vector2 upRightCorner;

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[UnityRandom.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wallTile;
                    if (x < downLeftCorner.x)
                        downLeftCorner.x = x;

                    if (y < downLeftCorner.y)
                        downLeftCorner.y = y;


                    if (x > upRightCorner.x)
                        upRightCorner.x = x;

                    if (y > upRightCorner.y)
                        upRightCorner.y = y;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize),
                    Quaternion.identity);

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public Vector3 getSpawnPoint()
    {
        if (spawners == null)
        {
            return calculateMapCenter();
        }

        var random = new SystemRandom(Convert.ToInt32(Time.time));
        return spawners.ElementAt(random.Next(0, spawners.Count)).transform.position;
    }

    private void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {
        BoardSetup();
        InitialiseSpawnPoints();
    }

    private void InitialiseSpawnPoints()
    {
        Vector3 center = calculateMapCenter();

        for (int i = 0; i < 5; i++)
        {
            var x = center.x + spawnersR * Math.Cos(2 * Math.PI * i / 5);
            var y = center.y + spawnersR * Math.Sin(2 * Math.PI * i / 5);

            var spawner = Instantiate(spawnerPrefab, new Vector3((float) x, (float) y), Quaternion.identity);
            spawners.Add(spawner);
        }
    }

    private Vector2 calculateMapCenter()
    {
        return (downLeftCorner * tileSize + upRightCorner * tileSize) / 2.0f;
    }
}