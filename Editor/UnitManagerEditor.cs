using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(UnitManager),true), CanEditMultipleObjects]
public class UnitManagerEditor : Editor
{
    UnitManager unitManager;
    private void OnEnable() {
        unitManager = (UnitManager)target;
    }
    private void OnSceneGUI() {
        DisplaySpawnedUnit();
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        DisplaySpawnedUnit();
    }
    void DisplaySpawnedUnit() {
        EditorGUILayout.LabelField("º“»Ø µ» ¿Ø¥÷");
        List<string> nameList = unitManager.GetSpawnedUnitNameList();
        for (int i = 0; i < nameList.Count; i++) {
            List<Unit> SpawnedUnitList = unitManager.GetSpawnedUnitList(nameList[i]);
            int UnitNum = SpawnedUnitList.Count;
            EditorGUILayout.LabelField(nameList[i] + " : " + UnitNum);
        }
    }
}
