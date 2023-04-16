using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Camera))]
public class CameraEffect : MonoBehaviour
{
    Camera m_Camera;
    void Awake() {
        TryGetComponent(out m_Camera);
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.Damaged, this, (eventType, sender, param) => { });
    }

    public void CameraShake(float duration, float strength, int vibrato, float randomness, bool fadeout) {
        m_Camera.DOShakePosition(duration, strength, vibrato, randomness, fadeout);
    }
}
