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
            SetUpPlayer(player, id);

            GameObject weapon = new GameObject("Laser");

            SetUpWeapon(weapon, id, player);
        }
    }

    private void SetUpWeapon(GameObject weapon, int id, GameObject player)
    {
        SetUpObjectSprite(weapon, "Graphic/laser");
        SetUpWeaponController(weapon, id);
        SetUpWeaponPosition(weapon, player);

        GameObject firePoint = new GameObject("FirePoint");
        firePoint.transform.parent = weapon.transform;
        firePoint.transform.localPosition = new Vector2(0.0f, 0.05f * weapon.transform.localScale.y);

        Weapon weaponScript = weapon.AddComponent<Weapon>();
        weaponScript.firePoint = firePoint.transform;
        weaponScript.playerId = id;
        weaponScript.impactEffectTemplate = GlobalObjects.smokeImpactEffect;
    }

    private void SetUpPlayer(GameObject player, int id)
    {
        SetUpPlayerPosition(player, id);
        SetUpObjectSprite(player, "Graphic/Character1");
        SetUpPlayerController(player, id);
        SetUpPlayerCollider(player);
    }

    private static void SetUpWeaponPosition(GameObject weapon, GameObject parent)
    {
        weapon.transform.parent = parent.transform;
        weapon.transform.localPosition = new Vector3(0.0f, 0.5f);
        weapon.transform.localScale = new Vector3(2.0f, 2.0f);
    }

    private static void SetUpWeaponController(GameObject weapon, int id)
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

    private void SetUpPlayerCollider(GameObject player)
    {
        player.AddComponent<CircleCollider2D>();
    }
}