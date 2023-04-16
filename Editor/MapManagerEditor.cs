using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapManager),true), CanEditMultipleObjects]
public class MapManagerEditor : Editor
{
    MapManager mapManager;

    SerializedProperty XDivisionProperty;
    SerializedProperty YDivisionProperty;
    private void OnEnable() {
        mapManager = (MapManager)target;

        XDivisionProperty = serializedObject.FindProperty("XDivision");
        YDivisionProperty = serializedObject.FindProperty("YDivision");
    }
    private void OnSceneGUI() {
        if(mapManager.RenderMapZone != null)
        DrawChuck();
    }
    void DrawChuck() {
        int XDivision = XDivisionProperty.intValue;
        int YDivision = YDivisionProperty.intValue;

        Vector3 center = mapManager.RenderMapZone.GetCenterVec3Pos();
        Vector2 mapSize = mapManager.RenderMapZone.GetSize();
        Vector3 leftDown = center - new Vector3(mapSize.x * 0.5f, 0, mapSize.y * 0.5f);
        Vector3 leftUp = center + new Vector3(mapSize.x * -0.5f, 0, mapSize.y * 0.5f);
        Vector3 rightUp = center + new Vector3(mapSize.x * 0.5f, 0, mapSize.y * 0.5f);
        Vector3 rightDowun = center + new Vector3(mapSize.x * 0.5f, 0, mapSize.y * -0.5f);

        Handles.color = Color.red;
        Handles.DrawLine(leftDown, leftUp);
        Handles.DrawLine(leftUp, rightUp);
        Handles.DrawLine(rightUp, rightDowun);
        Handles.DrawLine(rightDowun, leftDown);

        Vector3 startX = leftDown;
        float addDivisionX = mapSize.x / (XDivision + 1);
        for (int x = 0; x < XDivision; x++) {
            startX = startX + new Vector3(addDivisionX, 0, 0);
            Vector3 endPos = new Vector3(startX.x, 0, rightUp.z);
            Handles.DrawLine(startX, endPos);
        }

        Vector3 startY = leftDown;
        float addDivisionY = mapSize.y / (YDivision + 1);
        for (int y = 0; y < YDivision; y++) {
            startY = startY + new Vector3(0, 0, addDivisionY);
            Vector3 endPos = new Vector3(rightUp.x, 0, startY.z);
            Handles.DrawLine(startY, endPos);
        }
    }
    void DrawRegion() {

    }
}
