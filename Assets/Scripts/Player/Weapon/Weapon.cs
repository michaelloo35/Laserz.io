using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int playerId;
    public GameObject impactEffectTemplate;
    public float fireRate = 5f;

    private float timeToNextFire;

    void Update()
    {
        if (Input.GetAxis($"Fire_{playerId}") > 0.0f && Time.time > timeToNextFire)
        {
            timeToNextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        if (hitInfo)
        {
            GameObject impactEffect = Instantiate(impactEffectTemplate, hitInfo.point, hitInfo.transform.rotation);
            Destroy(impactEffect, 0.8f);
        }
    }
}