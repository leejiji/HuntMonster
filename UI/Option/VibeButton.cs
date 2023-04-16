using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
[RequireComponent(typeof(Button))]
public class VibeButton : MonoBehaviour, IInitializable
{
    [Inject] OptionData m_OptionData;
    public GameObject On;
    public GameObject Off;
    Button button;
    void Start()
    {
        Initialize();
    }
    void Click() {
        if (m_OptionData.isVibration) {
            m_OptionData.isVibration = false;
        }
        else {
            m_OptionData.isVibration = true;
        }
    }
    void Setting() {
        if (m_OptionData.isVibration) {
            On.gameObject.SetActive(true);
            Off.gameObject.SetActive(false);
        }
        else {
            On.gameObject.SetActive(false);
            Off.gameObject.SetActive(true);
        }
    }

    public void Initialize() {
        TryGetComponent(out button);
        button.onClick.AddListener(() => { Click(); Setting(); });
        Setting();
    }
}
