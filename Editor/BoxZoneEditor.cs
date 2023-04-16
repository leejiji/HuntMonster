using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;
[CustomEditor(typeof(BoxZone),true), CanEditMultipleObjects]
public class BoxZoneEditor : ZoneEditor
{
    BoxZone boxZone;

    protected override void OnEnable() {
        base.OnEnable();
        boxZone = (BoxZone)target;
        if(boxZone.Points.Count == 0) {
            boxZone.Points = new List<Vector3>();
            boxZone.AddPoint(-4, -4);
            boxZone.AddPoint(-4, 4);
            boxZone.AddPoint(4, 4);
            boxZone.AddPoint(4, -4);
            Debug.Log(boxZone.Points.Count);
        }
    }

    protected override void OnSceneGUI() {
        base.OnSceneGUI();
        Handles.BeginGUI();
        if(GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(Vector3.Lerp(boxZone.Points[0], boxZone.Points[1], 0.5f)),new Vector2(30,30)),
            Vector3.Distance(boxZone.Points[0], boxZone.Points[1]).ToString())) {
        }
        if (GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(Vector3.Lerp(boxZone.Points[1], boxZone.Points[2], 0.5f)), new Vector2(30, 30)),
    Vector3.Distance(boxZone.Points[1], boxZone.Points[2]).ToString())) {
        }
        Handles.EndGUI();
    }
    protected override void RemovePoint(int Index) {
    }

}
