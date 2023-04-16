using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class EndScore : MonoBehaviour
{
    [SerializeField] RectTransform Pannel;
    [SerializeField] TextMeshProUGUI MaxKillMonster;
    [SerializeField] TextMeshProUGUI MaxWave;
    [SerializeField] TextMeshProUGUI KillMonster;
    [SerializeField] TextMeshProUGUI Wave;

    int KillScore = 0;
    int WaveScore = 0;
    private void Awake() {
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Die, this, KillUpdate);
        EventManager<GameEvent>.Instance.AddListener(GameEvent.WaveStart, this, WaveUpdate);
        EventManager<GameEvent>.Instance.AddListener(GameEvent.GameEnd, this, ScoreUpdate);
    }
    void KillUpdate(MonsterEvent eventType, Component sender, object param) {
        KillScore++;
    }
    void WaveUpdate(GameEvent eventType, Component sender, object[] param) {
        WaveScore = (int)param[0];
    }
    void ScoreUpdate(GameEvent eventType, Component sender, object param) {
        SaveData loadData = SaveSystem.Load(1, "Score");
        int maxKillScore = 0;
        int maxWaveScore = 0;

        if (loadData != null) {
            SaveData maxKill = loadData.GetData("MaxKill");
            SaveData maxWave = loadData.GetData("MaxWave");
            maxKillScore = maxKill.GetInt();
            maxWaveScore = maxWave.GetInt();
        }
        KillMonster.text = KillScore.ToString();
        Wave.text = (WaveScore + 1).ToString();

        if (maxKillScore < KillScore)
            maxKillScore = KillScore;

        if (maxWaveScore < WaveScore)
            maxWaveScore = WaveScore;

        MaxKillMonster.text = maxKillScore.ToString();
        MaxWave.text = (maxWaveScore + 1).ToString();

        SaveData saveData = new SaveData();
        SaveData killData = new SaveData(maxKillScore);
        SaveData waveData = new SaveData(maxWaveScore);
        saveData.AddData("MaxKill", killData);
        saveData.AddData("MaxWave", waveData);
        SaveSystem.Save(1, "Score", saveData);

        Pannel.DOAnchorPos(Vector2.zero, 2);
    }
}
