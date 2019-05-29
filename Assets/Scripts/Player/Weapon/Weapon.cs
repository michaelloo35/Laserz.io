using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract String getName();
    public abstract Weapon Initialize(int playerId, PlayerStatus playerStatus);
    public abstract void Aim();
    public abstract void Shoot();
}