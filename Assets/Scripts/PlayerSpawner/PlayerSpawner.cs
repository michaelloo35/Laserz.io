using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayers(2);
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        for (int id = 0; id < numberOfPlayers; id++)
        {
            Instantiate(playerPrefab, new Vector3(0, id), Quaternion.identity)
                .GetComponent<PlayerController>()
                .Initialize(id);
        }
    }
}