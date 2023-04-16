using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MinimapCaptureCamera),true), CanEditMultipleObjects]
public class MinimapCaptureCameraEditor : Editor
{
    MinimapCaptureCamera minimapCaptureCamera;
    private void OnEnable() {
    minimapCaptureCamera = (MinimapCaptureCamera)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("¹Ì´Ï¸Ê Âï±â")) {
            minimapCaptureCamera.Capture();
        }
    }
}
