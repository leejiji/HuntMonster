using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public enum SoundType {
    Master, BGM, SFX
}
public class SoundSetting : ISaveable
{
    public AudioMixer audioMixer;
    Dictionary<SoundType, string> dic = new Dictionary<SoundType, string>();

    public SoundSetting(AudioMixer audioMixer) {
        this.audioMixer = audioMixer;
    }

    public void SetAudioVolume(SoundType soundType, float volume) {
        string audioMixerGroup = GetMixerName(soundType);
        if (volume == -40f) audioMixer.SetFloat(audioMixerGroup, -80);
        else audioMixer.SetFloat(audioMixerGroup, volume);
    }
    public float GetAudioVolume(SoundType soundType) {
        string audioMixerGroup = GetMixerName(soundType);
        float volume;
        audioMixer.GetFloat(audioMixerGroup,out volume);
        return volume;
    }
    string GetMixerName(SoundType type) {
        if (dic.ContainsKey(type))
            return dic[type];
        else {
            string name = type.ToString();
            dic.Add(type, name);
            return name;
        }

    }

    public void Save() {
        SaveData saveData = new SaveData();
        string[] soundNames = System.Enum.GetNames(typeof(SoundType));
        for(int i = 0; i < soundNames.Length; i++) {
            float volume;
            audioMixer.GetFloat(soundNames[i], out volume);
            SaveData soundData = new SaveData(volume);
            saveData.AddData(soundNames[i], soundData);
            Debug.Log(soundNames[i] + "세이브 " + volume);
        }
        SaveSystem.Save(1, "Option_SoundSetting", saveData);
    }

    public void Load() {
        SaveData loadData = SaveSystem.Load(1, "Option_SoundSetting");
        if (loadData == null)
            return;

        string[] soundNames = System.Enum.GetNames(typeof(SoundType));
        for (int i = 0; i < soundNames.Length; i++) {
            float volume = loadData.GetData(soundNames[i]).GetFloat();
            audioMixer.SetFloat(soundNames[i], volume);
            float getVolume;
            audioMixer.GetFloat(soundNames[i],out getVolume);
            Debug.Log(soundNames[i] + "로드 " + getVolume);
        }
    }
}