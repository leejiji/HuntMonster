using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "SOFadeImageData", menuName = "SO/FadeImage", order = 0)]
public class SOFadeImageData : ScriptableObject {
    [SerializeField] List<FadeImage> fadeImageList;
    [HideInInspector] public List<FadeImage> FadeImageList => fadeImageList;
}
[Serializable]
public struct FadeImage {
    public Sprite sprite;
    public string ImageName;
}