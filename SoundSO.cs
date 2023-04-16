using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Sound", order = 0)]

public class SoundSO : ScriptableObject
{
    public float MasterVolume;

    public float BGMVolume;

    public float SFXVolume;
}
