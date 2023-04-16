using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOMonster", menuName = "SO/Monster", order = 0)]
public class SOMonster : SOUnit
{
    [SerializeField, Range(0, 360)] float attackAngle = 0;
    public float AttackAngle => attackAngle;
    [SerializeField, Range(0.1f, 50)] float attackCul = 2;
    public float AttackCul => attackCul;
    [SerializeField] int minDropGold;
    public int MinDropGold => minDropGold;
    [SerializeField] int maxDropGold;
    public int MaxDropGold => maxDropGold;
}
