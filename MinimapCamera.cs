using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] float Height = 10;
    Vector3 pos = Vector3.zero;
    private void Awake() {
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.Spawn, this, SetTarget);
        pos = new Vector3(0, Height, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    void SetTarget(PlayerEvent eventType, Component sender, object param) {
        if(sender is Player) {
            Target = sender.transform;
            StartCoroutine(ChasingTarget());
        }
    }
    IEnumerator ChasingTarget() {
        while (true) {
            transform.position = Target.position + pos;
            yield return null;
        }
    }
}
