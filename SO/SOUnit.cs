using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOUnit", menuName = "SO/Unit", order = 0)]
public class SOUnit : ScriptableObject
{
    // 유닛 이름
    [SerializeField] string unitName;
    public string UnitName => unitName;
    // 미니맵 아이콘
    [SerializeField] Sprite minimapIcon;
    public Sprite MinimapIcon => minimapIcon;
    [SerializeField] string dieEffect;
    public string DieEffect => dieEffect;
    // 미니맵 아이콘 사이즈
    [SerializeField] float iconSize;
    public float IconSize => iconSize;
    // 최대 체력
    [SerializeField] int maxHp;
    public int MaxHP => maxHp;
    // 공격 데미지
    [SerializeField] int attackDamage;
    public int AttackDamage => attackDamage;
    // 공격 속도
    [SerializeField] float attackSpeed;
    public float AttackSpeed => attackSpeed;
    // 이동 속도
    [SerializeField] float moveSpeed;
    public float MoveSpeed => moveSpeed;
    // 공격 사거리
    [SerializeField] float attackRange;
    public float AttackRange => attackRange;
}