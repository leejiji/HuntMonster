using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon_Unit", menuName = "SO/Weapon/Weapon_Unit", order = 0)]

public class Weapon_Unit : ScriptableObject
{
    public string Weapon_name;

    public int Weapon_ID;

    public float Power;

    public float Speed;

    public float Reroad_speed;

    public int Bullet_cont;

    public bool Shot;
}
