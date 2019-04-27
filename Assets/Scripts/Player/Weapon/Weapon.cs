using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract Weapon Initialize(int playerId, PlayerStatus playerStatus);
    public abstract void Aim();
    public abstract void Shoot();
}