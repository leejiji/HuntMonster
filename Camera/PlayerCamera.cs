using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(CameraEffect))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 pos = Vector3.zero;

    Camera m_Camera;
    CameraEffect m_CameraEffect;
    [SerializeField] float Duration;
    [SerializeField] float Strength;
    [SerializeField] float UpdateTime;
    [SerializeField] bool DecreaseStrength;
    Vector3 Offset;

    private void Awake() {
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.Spawn, this, SetTarget);
        TryGetComponent(out m_Camera);
        TryGetComponent(out m_CameraEffect);
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.Damaged, this, (eventType, sender, param) => { CameraShake(Duration, Strength, UpdateTime, DecreaseStrength); });
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            CameraShake(Duration, Strength, UpdateTime, DecreaseStrength);
        }
    }
    void SetTarget(PlayerEvent eventType, Component sender, object param) {
        if (sender is Player) {
            Target = sender.transform;
            StartCoroutine(ChasingTarget());
        }
    }
    IEnumerator ChasingTarget() {
        while (true) {
            transform.position = Target.position + pos + Offset;
            yield return null;
        }
    }
    public void CameraShake(float duration, float strength,float updateTime, bool decreaseStrength) {
        StartCoroutine(C_ShakeCamera(duration, strength, updateTime, decreaseStrength));
    }
    IEnumerator C_ShakeCamera(float duration, float strength, float updateTime, bool decreaseStrength) {
        float lastTime = duration;
        float lastUpdateTime = 0;
        float power = strength;
        while (lastTime > 0) {
            lastTime -= Time.deltaTime;
            lastUpdateTime -= Time.deltaTime;
            if (lastUpdateTime <= 0) {
                if (decreaseStrength)
                    power = strength * (lastTime / duration);
                Offset = new Vector3(Random.Range(-power, power), Random.Range(-power, power), 0);

                lastUpdateTime = updateTime;
            }
            yield return null;
        }
    }
}
