using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int playerId;
    void Update()
    {
        Input.GetButtonDown($"Fire_{playerId}");
    }
}