using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOUnit", menuName = "SO/Unit", order = 0)]
public class SOUnit : ScriptableObject
{
    // ���� �̸�
    [SerializeField] string unitName;
    public string UnitName => unitName;
    // �̴ϸ� ������
    [SerializeField] Sprite minimapIcon;
    public Sprite MinimapIcon => minimapIcon;
    [SerializeField] string dieEffect;
    public string DieEffect => dieEffect;
    // �̴ϸ� ������ ������
    [SerializeField] float iconSize;
    public float IconSize => iconSize;
    // �ִ� ü��
    [SerializeField] int maxHp;
    public int MaxHP => maxHp;
    // ���� ������
    [SerializeField] int attackDamage;
    public int AttackDamage => attackDamage;
    // ���� �ӵ�
    [SerializeField] float attackSpeed;
    public float AttackSpeed => attackSpeed;
    // �̵� �ӵ�
    [SerializeField] float moveSpeed;
    public float MoveSpeed => moveSpeed;
    // ���� ��Ÿ�
    [SerializeField] float attackRange;
    public float AttackRange => attackRange;
}