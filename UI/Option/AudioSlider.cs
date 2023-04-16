using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;
[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] SoundType Type;
    [Inject] OptionData m_OptionData;
    private void Start() {
        TryGetComponent(out slider);
        slider.minValue = -40;
        slider.maxValue = 0;

        float volume = m_OptionData.m_SoundSetting.GetAudioVolume(Type);
        slider.value = volume;
        slider.onValueChanged.AddListener((value) => { m_OptionData.m_SoundSetting.SetAudioVolume(Type, value);});
    }
}
