using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalObjects.init();
        SpawnPlayers(2);
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        for (int id = 0; id < numberOfPlayers; id++)
        {
            GameObject player = new GameObject($"Player{id}");
            PlayerStatus playerStatus = new PlayerStatus();
            SetUpPlayer(player, id, playerStatus);
        }
    }

    private void SetUpPlayer(GameObject player, int id, PlayerStatus playerStatus)
    {
        SetUpPlayerPosition(player, id);
        SetUpObjectSprite(player, "Graphic/Character1");
        SetUpPlayerController(player, id, playerStatus);
        SetUpPlayerCollider(player);
    }

    private void SetUpPlayerPosition(GameObject player, int id)
    {
        player.transform.localPosition = new Vector3(0.0f + id, 0.0f);
    }

    private void SetUpObjectSprite(GameObject obj, string spritePath)
    {
        SpriteRenderer playerSpriteRenderer = obj.AddComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = Resources.Load<Sprite>(spritePath);
    }

    private void SetUpPlayerController(GameObject player, int id, PlayerStatus playerStatus)
    {
        PlayerController playerController = player.AddComponent<PlayerController>();
        playerController.playerId = id;
        playerController.playerStatus = playerStatus;
    }

    private void SetUpPlayerCollider(GameObject player)
    {
        player.AddComponent<CircleCollider2D>();
    }
}