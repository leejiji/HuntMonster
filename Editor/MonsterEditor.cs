using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Monster),true),CanEditMultipleObjects]
public class MonsterEditor : UnitEditor
{
    Monster monster;
    SOMonster MonsterData;
    SerializedProperty meshRenderer;
    public override void OnEnable() {
        base.OnEnable();
        monster = (Monster)target;
        meshRenderer = serializedObject.FindProperty("meshRenderer");
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (monster.SOUnitData != null) {
            if (MonsterData == null)
                MonsterData = (SOMonster)monster.SOUnitData;
            EditorGUILayout.LabelField("최대 체력 : " + MonsterData.MaxHP);
            EditorGUILayout.LabelField("현재 체력 : " + monster.HP);
            EditorGUILayout.LabelField("이동 속도 : " + MonsterData.MoveSpeed);
            EditorGUILayout.LabelField("공격 속도 : " + MonsterData.AttackSpeed);
            EditorGUILayout.LabelField("공격 사거리 : " + MonsterData.AttackRange);
            EditorGUILayout.LabelField("최소 드롭 골드 : " + MonsterData.MinDropGold);
            EditorGUILayout.LabelField("최대 드롭 골드 : " + MonsterData.MaxDropGold);
        }
    }
}
