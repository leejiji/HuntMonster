using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SOFadeData", menuName = "SO/FadeEffect", order = 0)]
public class SOFadeData : ScriptableObject
{
    [SerializeField] List<FadeData> fadeDataList;
    [HideInInspector] public List<FadeData> FadeDataList => fadeDataList;
}
[Serializable]
public struct FadeData {
    public FadeInOut FadeInOut;
    public string EffectName;
}