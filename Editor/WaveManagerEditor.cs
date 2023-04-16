using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(WaveManager),true), CanEditMultipleObjects]
public class WaveManagerEditor : Editor
{
    WaveManager waveManager;

    SerializedProperty soWaveDataProperty;
    SerializedProperty waitTimerProperty;
    ReorderableList zoneList;
    ReorderableList waveList;
    private void OnEnable() {
        waveManager = (WaveManager)target;
        waitTimerProperty = serializedObject.FindProperty("WaitTimer");
        soWaveDataProperty = serializedObject.FindProperty("soWaveData");

        waveManager.WaveDatas = waveManager.soWaveData.WaveDatas;
        ZoneListSetting();
        WaveListSetting();
    }
    void ZoneListSetting() {
        SerializedProperty zoneListProperty = serializedObject.FindProperty("SpawnZoneList");
        zoneList = new ReorderableList(serializedObject, zoneListProperty, true, true, true, true);

        zoneList.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, "���� ���� ����");
        };
        zoneList.drawElementCallback = (rect, index, isActive, isFocused) => {
            EditorGUIUtility.labelWidth = 80;
            EditorGUI.PropertyField(rect, zoneListProperty.GetArrayElementAtIndex(index));
        };
    }
    void WaveListSetting() {
        SerializedProperty waveListProperty = serializedObject.FindProperty("WaveDatas");
        waveList = new ReorderableList(serializedObject, waveListProperty, true, true, true, true);

        waveList.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, "���̺� ���� ��");
        };
        waveList.elementHeight += 18;
        waveList.drawElementCallback = (rect, index, isActive, isFocused) => {
            EditorGUIUtility.labelWidth = 80;
            rect.y -= 10;
            EditorGUI.LabelField(rect, (index + 1 + " ��° ���̺�"));
            rect.y += 28;
            rect.height = 18;
            GUIContent label = new GUIContent("���� ����");
            EditorGUI.PropertyField(rect, waveListProperty.GetArrayElementAtIndex(index).FindPropertyRelative("monsterNum"), label);
        };
        waveList.draggable = false;
    }
    public override void OnInspectorGUI() {
        if (zoneList != null) {
            zoneList.DoLayoutList();
        }
        EditorGUIUtility.labelWidth += 20;
        EditorGUILayout.PropertyField(soWaveDataProperty);
        serializedObject.ApplyModifiedProperties();
        if (waveList != null && waveManager.soWaveData != null) {
            EditorGUILayout.PropertyField(waitTimerProperty);
            serializedObject.ApplyModifiedProperties();
            waveList.DoLayoutList();
        }
        EditorGUILayout.LabelField("��ȯ ��� ���� ���� : " + serializedObject.FindProperty("LastWaveMonsterNum").intValue);
    }
}
