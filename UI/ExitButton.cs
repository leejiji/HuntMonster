using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ExitButton : MonoBehaviour
{
    Button button;
    void Start() {
        TryGetComponent(out button);
        button.onClick.AddListener(() => { Application.Quit(); });
    }
}
