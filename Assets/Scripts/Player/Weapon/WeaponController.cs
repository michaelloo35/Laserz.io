using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int playerId;

    void Update()
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

    private Quaternion UpdateWeaponRotation(PlayerAimInputs playerAimInputs)
    {
        float rotationDegrees = 180 + Mathf.Rad2Deg * Mathf.Atan2(playerAimInputs.AimXAxis, playerAimInputs.AimYAxis);

        return transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotationDegrees));
    }
}