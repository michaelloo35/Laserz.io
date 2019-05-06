using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int columns = 16;

    public int rows = 12;

    private float tileSize = 0.32f;

    public Count wallCount = new Count(50, 80);

    public GameObject[] floorTiles;

    public GameObject wallTile;

    private Transform boardHolder;

    private List<Vector3> gridPositions = new List<Vector3>();

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x * tileSize, y * tileSize, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wallTile;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f),
                    Quaternion.identity);

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject tile, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            Instantiate(tile, randomPosition, Quaternion.identity);
        }
    }


    private void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {
        BoardSetup();
//        InitialiseList();
//        LayoutObjectAtRandom(wallTile, wallCount.minimum, wallCount.maximum);
    }
}