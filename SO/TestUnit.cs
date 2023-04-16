using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : MonoBehaviour
{
    [SerializeField] SOTestUnitData data;
}
public class SOTestUnitData : ScriptableObject {
    public int HP;
    public int Speed;
    public int Power;
}