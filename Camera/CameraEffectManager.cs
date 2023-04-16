using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraEffectManager : MonoBehaviour
{
    public void CameraShake(float duration, float strength, int vibrato, float randomness, bool fadeout) {
        Camera mainCamera = Camera.main;
        mainCamera.DOShakePosition(duration, strength, vibrato, randomness, fadeout);
    }
}
