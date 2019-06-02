using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerController playerPrefab;
    private Transform[] players;
    private readonly int playerCount = 2;


    // Start is called before the first frame update
    void Start()
    {
        players = new Transform[playerCount];
        SpawnPlayers(playerCount);
        InitializeCameraManager();
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        for (int id = 0; id < numberOfPlayers; id++)
        {
            var player = Instantiate(playerPrefab, new Vector3(id * 5, 0), Quaternion.identity);
            player.Initialize(id, gameObject.GetComponent<BoardManager>());
            players[id] = player.transform;
        }
    }


    private void InitializeCameraManager()
    {
        GetComponent<CameraManager>().Initialize(players);
    }
}