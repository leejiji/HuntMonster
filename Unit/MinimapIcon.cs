using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    public void SettingIcon(Sprite sprite, float scale) {
        SpriteRenderer renderer;
        TryGetComponent(out renderer);
        renderer.sprite = sprite;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
