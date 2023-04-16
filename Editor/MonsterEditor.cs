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
            EditorGUILayout.LabelField("�ִ� ü�� : " + MonsterData.MaxHP);
            EditorGUILayout.LabelField("���� ü�� : " + monster.HP);
            EditorGUILayout.LabelField("�̵� �ӵ� : " + MonsterData.MoveSpeed);
            EditorGUILayout.LabelField("���� �ӵ� : " + MonsterData.AttackSpeed);
            EditorGUILayout.LabelField("���� ��Ÿ� : " + MonsterData.AttackRange);
            EditorGUILayout.LabelField("�ּ� ��� ��� : " + MonsterData.MinDropGold);
            EditorGUILayout.LabelField("�ִ� ��� ��� : " + MonsterData.MaxDropGold);
        }
    }
}
