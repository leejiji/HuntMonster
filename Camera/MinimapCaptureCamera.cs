using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MinimapCaptureCamera : MonoBehaviour
{
    Camera captureCamera;
    public Camera CaptureCamera => captureCamera;
    [SerializeField] RenderTexture MinimapTexture;
    void Awake()
    {
        TryGetComponent(out captureCamera);
        Capture();
        CaptureCamera.gameObject.SetActive(false);
    }
    public void Capture() {
        TryGetComponent(out captureCamera);
        RenderTexture cameraTexture = CaptureCamera.targetTexture;
        CaptureCamera.targetTexture = MinimapTexture;
        CaptureCamera.Render();
        CaptureCamera.targetTexture = cameraTexture;
    }
}
