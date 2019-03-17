using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayers(2);
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        for (int id = 0; id < numberOfPlayers; id++)
        {
            GameObject player = new GameObject($"Player{id}");

            SetUpPlayerPosition(player, id);
            SetUpObjectSprite(player, "Graphic/Character1");
            SetUpPlayerController(player, id);
            SetUpPlayerCollider(player, id);

            GameObject weapon = new GameObject("laser");

            SetUpObjectSprite(weapon, "Graphic/laser");
            setUpWeaponController(weapon, id);
            SetUpWeaponPosition(weapon, player);
        }
    }

    private static void SetUpWeaponPosition(GameObject weapon, GameObject parent)
    {
        weapon.transform.parent = parent.transform;
        weapon.transform.localPosition = new Vector3(0.0f, 0.5f);
        weapon.transform.localScale = new Vector3(2.0f, 2.0f);
    }

    private static void setUpWeaponController(GameObject weapon, int id)
    {
        WeaponController weaponController = weapon.AddComponent<WeaponController>();
        weaponController.playerId = id;
    }

    private void SetUpPlayerPosition(GameObject player, int id)
    {
        player.transform.localPosition = new Vector3(0.0f + id, 0.0f);
    }

    private void SetUpObjectSprite(GameObject player, string spritePath)
    {
        SpriteRenderer playerSpriteRenderer = player.AddComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = Resources.Load<Sprite>(spritePath);
    }

    private void SetUpPlayerController(GameObject player, int id)
    {
        PlayerController playerController = player.AddComponent<PlayerController>();
        playerController.playerId = id;
    }

    private void SetUpPlayerCollider(GameObject player, int id)
    {
        player.AddComponent<CircleCollider2D>();
    }
}