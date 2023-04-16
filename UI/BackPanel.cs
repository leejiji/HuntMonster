using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Open();
        }
    }

    public void Close() {
        Time.timeScale = 1;
        Panel.SetActive(false);
    }
    public void Open() {
        Time.timeScale = 0;
        Panel.SetActive(true);
    }
}
