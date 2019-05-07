using System;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public PlayerStatus playerStatus;
    public Transform firePoint;
    public float fireRate;
    public float damage;
    public float damageDropRatio = 0.8f;
    public int spread;
    public float shootingDistance;
    private float density = 1.0f;
    private int playerId;
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
            Fire();
            timeToNextFire = Time.time + 1f / fireRate;
        }
    }


    private void Fire()
    {
        DrawShootingEffect();
        DealDamage();
    }

    private void DrawShootingEffect()
    {
        for (int i = -spread; i < spread; i++)
        {
            var shootAngle = Quaternion.Euler(0, 0, i * density) * firePoint.up;
            DrawLine(firePoint.position, firePoint.position + shootAngle.normalized * shootingDistance);
        }
    }

    private void DealDamage()
    {
        Dictionary<PlayerController, float> dmgMap = new Dictionary<PlayerController, float>();
        for (int i = -spread; i < spread; i++)
        {
            var shootAngle = Quaternion.Euler(0, 0, i * density) * firePoint.up;
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, shootAngle, shootingDistance);

            if (hitInfo && hitInfo.collider.CompareTag("Player"))
            {
                var player = hitInfo.collider.gameObject.GetComponent<PlayerController>();
                if (dmgMap.ContainsKey(player))
                {
                    dmgMap[player] += damage;
                }
                else
                {
                    dmgMap[player] = damage;
                }
            }
        }

        foreach (var item in dmgMap)
        {
            item.Key.TakeDamage(item.Value);
        }
    }

    private void DrawLine(Vector3 start, Vector3 end, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Hidden/Internal-Halo"));
        lr.material.renderQueue = 5000;
        var width = 0.15f;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.startColor = Color.red;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, duration);
    }
}