using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class SoundPlayer : MonoBehaviour
{
    [Inject] IPlaySound m_PlaySound;
    [SerializeField] SoundType SoundType;
    [SerializeField] string Clip;
    void Start()
    {
        m_PlaySound.PlaySound(SoundType, Clip);
    }
}
