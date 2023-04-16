using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(fileName = "SOSoundData", menuName = "SO/SoundData", order = 0)]
public class SOSoundData : ScriptableObject
{
    [SerializeField] List<SoundData> datas;
    public List<SoundData> Datas => datas;
}
[System.Serializable]
public struct SoundData {
    public AudioMixerGroup outputAudioMixerGroup;
    public bool isLoop;
    [SerializeField] SoundType type;
    public SoundType Type => type;

    [SerializeField] List<AudioClip> audioClips;
    public List<AudioClip> AudioClips => audioClips;
}
