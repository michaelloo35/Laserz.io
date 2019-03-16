using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("playerName")] public int playerId;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var inputXAxisMovement = Input.GetAxis($"Horizontal_{playerId}");
        var inputYAxisMovement = Input.GetAxis($"Vertical_{playerId}");
 
        MoveXY(inputXAxisMovement, inputYAxisMovement);

        var aimXAxisDirection = Input.GetAxis($"Aim_Horizontal_{playerId}");
        var aimYAxisDirection = Input.GetAxis($"Aim_Vertical_{playerId}");
        AimWeapon(aimXAxisDirection, aimYAxisDirection);
    }

    private void MoveXY(float inputXAxis, float inputYAxis)
    {
        Vector3 moveVector = new Vector3(inputXAxis, inputYAxis, 0.0f);

        transform.position += moveVector * Time.deltaTime;
    }

    private void AimWeapon(float aimXAxis, float aimYAxis)
    {
        if (Math.Abs(aimXAxis) > 0.01f && Math.Abs(aimYAxis) > 0.01f)
        {
            Vector3 aim = new Vector3(aimXAxis, -aimYAxis, 0.0f);
            if (aim.magnitude > 0.0f)
                aim.Normalize();
            aim = aim * 0.5f;
            weapon.transform.localPosition = aim;
            weapon.transform.localRotation =
                Quaternion.Euler(new Vector3(0.0f, 0.0f, 180 + Mathf.Rad2Deg * Mathf.Atan2(aimXAxis, aimYAxis)));
        }
    }
}