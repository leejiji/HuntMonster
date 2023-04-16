using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOJoyStickValue", menuName = "SO/UI/JoyStickValue", order = 0)]
public class SOJoyStickValue : ScriptableObject
{
    public Vector2 Value;
    public bool Playing;
}
