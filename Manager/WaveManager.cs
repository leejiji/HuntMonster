using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Zone> SpawnZoneList = new List<Zone>();
    [SerializeField] public SOWaveData soWaveData;
    [SerializeField] public List<WaveData> WaveDatas;
    [SerializeField] float NextSpawnCul = 4;
    [SerializeField] float NextWaveCul = 5;

    [SerializeField] int LastSpawnedMonsterNum = 0; // 필드에 소환된 몬스터 수
    [SerializeField] int LastWaveMonsterNum; // 남은 소환 할 웨이브 몬스터

    [SerializeField] bool WaitTimer = false;
    List<Unit> UnitList = new List<Unit>();
    int CurrentWave = 0;

    bool isSpawnEnd = false;

    [Inject] IPlaySound PlaySound;

    WaitForSeconds WaitSpawn;
    WaitForSeconds WaitNextWave;
    private void Awake() {
        WaitSpawn = new WaitForSeconds(NextSpawnCul);
        WaitNextWave = new WaitForSeconds(NextWaveCul);
        UnitList = UnitManager.Instance.GetSpawnableUnitList();
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Spawn, this, SpawnSensing);
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Die, this, DieSensing);
        if (WaitTimer) {
            EventManager<GameEvent>.Instance.AddListener(GameEvent.TimerEnd, this, (eventType, sender, param) => { PlaySound.PlaySound(SoundType.BGM, "Play"); StartCoroutine(C_WaveStart(CurrentWave)); });
        }
    }
    private void Start() {
        if(!WaitTimer)
        StartCoroutine(C_WaveStart(CurrentWave));
    }
    void SpawnSensing(MonsterEvent eventType, Component sender, object param) {
        LastSpawnedMonsterNum++;
    }
    void DieSensing(MonsterEvent eventType, Component sender, object param) {
        LastSpawnedMonsterNum--;
        if (isSpawnEnd == true && LastSpawnedMonsterNum <= 0) {
            if (CurrentWave < soWaveData.WaveDatas.Count)
                StartCoroutine(C_WaitNextWave());
        }
    }

    IEnumerator C_WaveStart(int wave) {
        isSpawnEnd = false;
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.WaveStart, this, new object[] { wave });

        if(wave != 0)
        PlaySound.PlayOneShot(SoundType.SFX, "NextWave");
        LastWaveMonsterNum = soWaveData.WaveDatas[wave].MonsterNum;
        Debug.Log(wave + 1 + "웨이브 시작");
        while (LastWaveMonsterNum > 0) {
            int SpawnMonsetNum;
            if(LastWaveMonsterNum > 10) {
                SpawnMonsetNum = 10;
                LastWaveMonsterNum -= 10;
            }
            else {
                SpawnMonsetNum = LastWaveMonsterNum;
                LastWaveMonsterNum = 0;
            }

            for(int i = 0; i < SpawnMonsetNum; i++) {
                SpawnMonster();
            }
            yield return WaitSpawn;
        }
        isSpawnEnd = true;
        Debug.Log(wave + 1 + "웨이브 몬스터 소환 종료");
    }
    IEnumerator C_WaitNextWave() {
        Debug.Log(CurrentWave + 1 + "웨이브 종료");
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.WaveEnd, this, new object[] { CurrentWave});
        if (CurrentWave < soWaveData.WaveDatas.Count) {
            PlaySound.PlaySound(SoundType.SFX, "Timer", NextWaveCul);
            yield return WaitNextWave;
            CurrentWave++;
            StartCoroutine(C_WaveStart(CurrentWave));
        }
        else {
            EventManager<GameEvent>.Instance.PostEvent(GameEvent.GameEnd, this, null);
        }
    }
    void SpawnMonster() {
        Unit randomUnit = UnitList[Random.Range(0, UnitList.Count)];
        int spawnZoneIndex = Random.Range(0, SpawnZoneList.Count);

        Zone spawnZone = SpawnZoneList[spawnZoneIndex];
        Vector3 spawnPoint = spawnZone.GetRandomPointInZone();

        UnitManager.Instance.SpawnUnit(randomUnit.SOUnitData.UnitName, spawnPoint);
    }

}
