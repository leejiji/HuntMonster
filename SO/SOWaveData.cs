using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOWaveData", menuName = "SO/WaveData", order = 0), System.Serializable]
public class SOWaveData : ScriptableObject
{
    [SerializeField] List<WaveData> waveDatas;
    public List<WaveData> WaveDatas => waveDatas;
}
[System.Serializable]
public class WaveData {
    [SerializeField] int monsterNum;
    public int MonsterNum => monsterNum;
    [SerializeField] float nextWaveTime;
    public float NextWaveTime => nextWaveTime;
}