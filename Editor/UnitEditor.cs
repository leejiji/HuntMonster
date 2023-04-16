using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Unit), true), CanEditMultipleObjects]
public class UnitEditor : Editor
{
    Unit unit;
    Collider collider;
    public virtual void OnEnable() {
        unit = (Unit)target;
        unit.TryGetComponent(out collider);
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(collider != null && !unit.Positioning)
        unit.transform.position = new Vector3(unit.transform.position.x, collider.bounds.size.y / 2, unit.transform.position.z);
    }
}
