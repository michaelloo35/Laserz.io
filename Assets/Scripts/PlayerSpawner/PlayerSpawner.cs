using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GlobalObjects GlobalObjects;

    // Start is called before the first frame update
    void Start()
    {
        GlobalObjects = new GlobalObjects();
        SpawnPlayers(2);
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        for (int id = 0; id < numberOfPlayers; id++)
        {
            GameObject player = new GameObject($"Player{id}");
            PlayerStatus playerStatus = new PlayerStatus();
            SetUpPlayer(player, id, playerStatus);

            GameObject weapon = new GameObject("Laser");

            SetUpWeapon(weapon, id, player, playerStatus);
        }
    }

    private void SetUpWeapon(GameObject weapon, int id, GameObject player, PlayerStatus playerStatus)
    {
        SetUpObjectSprite(weapon, "Graphic/laser");
        SetUpWeaponController(weapon, id, playerStatus);
        SetUpWeaponPosition(weapon, player);

        GameObject firePoint = new GameObject("FirePoint");
        firePoint.transform.parent = weapon.transform;
        firePoint.transform.localPosition = new Vector2(0.0f, 0.05f * weapon.transform.localScale.y);

        Laser laserScript = weapon.AddComponent<Laser>();
        laserScript.firePoint = firePoint.transform;
        laserScript.playerId = id;
        laserScript.impactEffectTemplate = GlobalObjects.smokeImpactEffect;
        laserScript.playerStatus = playerStatus;
    }

    private void SetUpPlayer(GameObject player, int id, PlayerStatus playerStatus)
    {
        SetUpPlayerPosition(player, id);
        SetUpObjectSprite(player, "Graphic/Character1");
        SetUpPlayerController(player, id, playerStatus);
        SetUpPlayerCollider(player);
    }

    private static void SetUpWeaponPosition(GameObject weapon, GameObject parent)
    {
        weapon.transform.parent = parent.transform;
        weapon.transform.localPosition = new Vector3(0.0f, 0.5f);
        weapon.transform.localScale = new Vector3(2.0f, 2.0f);
    }

    private static void SetUpWeaponController(GameObject weapon, int id, PlayerStatus playerStatus)
    {
        WeaponController weaponController = weapon.AddComponent<WeaponController>();
        weaponController.playerId = id;
        weaponController.playerStatus = playerStatus;
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

    private void SetUpPlayerController(GameObject player, int id, PlayerStatus playerStatus)
    {
        PlayerController playerController = player.AddComponent<PlayerController>();
        playerController.playerId = id;
        playerController.playerStatus = playerStatus;
        playerController.deathEffectTemplate = GlobalObjects.deathAnimation;
    }

    private void SetUpPlayerCollider(GameObject player)
    {
        player.AddComponent<CircleCollider2D>();
    }
}