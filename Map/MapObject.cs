using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
   [SerializeField] bool isActiveCollider; // false 일시 한번 꺼지면 다신 안켜짐
   [SerializeField] bool UnActiveColliderInUnActive; 
   [SerializeField] MeshRenderer[] meshRenderer;
   [SerializeField] Collider ObjectCollider;
    private void Start() {
        if(!isActiveCollider)
            ObjectCollider.enabled = false;
    }
    public void SetActiveRenderer(bool active) {
        for(int i = 0; i < meshRenderer.Length; i++) {
            meshRenderer[i].enabled = active;
        }
        if (isActiveCollider) {
            if (UnActiveColliderInUnActive) {
                ObjectCollider.enabled = active;
            }
        }
        else
            ObjectCollider.enabled = false;
        
    }
}
