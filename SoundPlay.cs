using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundPlay : MonoBehaviour, IPlaySound {
    [SerializeField] SOSoundData soSoundData;
    Dictionary<SoundType, AudioSource> AudioSourceDic = new Dictionary<SoundType, AudioSource>();
    Dictionary<SoundType, Dictionary<string, AudioClip>> AudioClipDic = new Dictionary<SoundType, Dictionary<string, AudioClip>>();
    public void Awake() {
        Init();
    }
    void Init() {
        AudioClipDic = new Dictionary<SoundType, Dictionary<string, AudioClip>>();
        string[] soundNames = new string[soSoundData.Datas.Count];
        for (int i = 0; i < soSoundData.Datas.Count; i++) {
            soundNames[i] = System.Enum.GetName(typeof(SoundType), soSoundData.Datas[i].Type);
        }
        int soundTypeCount = soundNames.Length;

        for (int i = 0; i < soundTypeCount; i++) {

            AudioSource audioSource = new GameObject(soundNames[i] + "Source").AddComponent<AudioSource>();
            audioSource.transform.parent = transform;
            audioSource.outputAudioMixerGroup = soSoundData.Datas[i].outputAudioMixerGroup;

            if (soSoundData.Datas[i].isLoop)
                audioSource.loop = true;
            SoundType soundType = soSoundData.Datas[i].Type;

            // 오디오 소스 딕셔너리에 추가
            if (!AudioSourceDic.ContainsKey(soundType)) {
                AudioSourceDic.Add(soundType, audioSource);
            }
            // 클립 딕셔너리에 추가
            Dictionary<string, AudioClip> audioClipDIc = new Dictionary<string, AudioClip>();
            for (int k = 0; k < soSoundData.Datas[i].AudioClips.Count; k++) {
                string clipName = soSoundData.Datas[i].AudioClips[k].name;
                AudioClip clip = soSoundData.Datas[i].AudioClips[k];
                audioClipDIc.Add(clipName, clip);
            }
            AudioClipDic.Add(soundType, audioClipDIc);
        }
    }
    public void PlaySound(SoundType soundType, string clipName) {
        AudioSource clipPlayer = new GameObject("ClipPlayer").AddComponent<AudioSource>();
        clipPlayer.transform.parent = transform;
        clipPlayer.outputAudioMixerGroup = AudioSourceDic[soundType].outputAudioMixerGroup;
        bool loop = AudioSourceDic[soundType].loop;
        clipPlayer.loop = loop;

        AudioClip clip = GetAudioClip(soundType, clipName);
        clipPlayer.clip = clip;
        clipPlayer.Play();

        if(!loop)
        Destroy(clipPlayer, clip.length + 1);
    }
    public void PlaySound(SoundType soundType, string clipName, float timer) {
        AudioSource clipPlayer = new GameObject("ClipPlayer").AddComponent<AudioSource>();
        clipPlayer.transform.parent = transform;
        AudioClip clip = GetAudioClip(soundType, clipName);
        clipPlayer.clip = clip;
        clipPlayer.Play();
        Destroy(clipPlayer, timer);
    }
    public void PlayOneShot(SoundType soundType, string clipName) {
        AudioSource audiosource = AudioSourceDic[soundType];
        AudioClip clip = GetAudioClip(soundType, clipName);
        audiosource.PlayOneShot(clip);
    }
    public AudioClip GetAudioClip(SoundType soundType, string clipName) {
        if (AudioClipDic.ContainsKey(soundType)) {
            if (AudioClipDic[soundType].ContainsKey(clipName)) {
                AudioClip clip = AudioClipDic[soundType][clipName];
                return clip;
            }
            else
            {
                Debug.LogWarning(soundType + "에 " + clipName + "이라는 오디오 클립은 존재하지 않습니다.");
                return null;
            }
        }
        else {
            Debug.LogWarning(soundType + "은 오디오 소스가 존재하지 않습니다.");
            return null;
        }
    }
}

public interface IPlaySound {
    public void PlaySound(SoundType soundType, string clipName);
    public void PlaySound(SoundType soundType, string clipName, float timer);
    public void PlayOneShot(SoundType soundType, string clipName);

}
