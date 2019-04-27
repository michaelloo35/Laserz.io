using System;
using UnityEngine;

public class Laser : Weapon
{
    public PlayerStatus playerStatus;
    public Transform firePoint;
    public float fireRate = 5f;
    public GameObject impactEffectPrefab;
    private int playerId;
    private float charge;
    private float timeToNextFire;


    public override Weapon Initialize(int playerId, PlayerStatus playerStatus)
    {
        this.playerId = playerId;
        this.playerStatus = playerStatus;
        return this;
    }

    public override void Aim()
    {
        PlayerAimInputs playerAimInputs = new PlayerAimInputs(playerId);
        AimWeapon(playerAimInputs);
    }

    private void AimWeapon(PlayerAimInputs playerAimInputs)
    {
        if (Math.Abs(playerAimInputs.AimXAxis) > 0.01f && Math.Abs(playerAimInputs.AimYAxis) > 0.01f)
        {
            UpdateWeaponPosition(playerAimInputs);
            UpdateWeaponRotation(playerAimInputs);
        }
    }

    private void UpdateWeaponPosition(PlayerAimInputs playerAimInputs)
    {
        Vector3 aimVector = new Vector3(playerAimInputs.AimXAxis, -playerAimInputs.AimYAxis, 0.0f);

        if (aimVector.magnitude > 0.0f)
            aimVector.Normalize();
        aimVector = aimVector * 0.5f;
        transform.localPosition = aimVector;
    }

    private void UpdateWeaponRotation(PlayerAimInputs playerAimInputs)
    {
        float rotationDegrees = 180 + Mathf.Rad2Deg * Mathf.Atan2(playerAimInputs.AimXAxis, playerAimInputs.AimYAxis);

        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotationDegrees));
    }

    public override void Shoot()
    {
        PlayerShootInputs playerShootInputs = new PlayerShootInputs(playerId);
        if (playerShootInputs.shoot > 0 && Time.time > timeToNextFire)
        {
            playerStatus.blocked = true;
            charge += Time.deltaTime;
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
            if (hitInfo)
            {
                DrawLine(firePoint.position, hitInfo.point, charge / 5.0f);
            }
            else
            {
                DrawLine(firePoint.position, firePoint.up.normalized * 999, charge / 5.0f);
            }

            if (charge > 1.0f)
            {
                Fire();
                charge = 0.0f;
                timeToNextFire = Time.time + 1f / fireRate;
                playerStatus.blocked = false;
            }
        }
        else
        {
            charge = 0;
            playerStatus.blocked = false;
        }
    }


    private void Fire()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        if (hitInfo)
        {
            ImpactEffect(hitInfo);
            hitInfo.collider.gameObject.GetComponent<PlayerController>().Die();
        }
    }

    private void ImpactEffect(RaycastHit2D hitInfo)
    {
        var impactEffect = Instantiate(
            impactEffectPrefab,
            hitInfo.point,
            Quaternion.FromToRotation(transform.up, hitInfo.normal) * transform.rotation);
        Destroy(impactEffect, 0.8f);
    }

    void DrawLine(Vector3 start, Vector3 end, float width, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startWidth = width / 2;
        lr.endWidth = width / 2;
        lr.startColor = Color.magenta;
        lr.endColor = Color.blue;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, duration);
    }
}