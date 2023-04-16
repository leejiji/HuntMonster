using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class NextWaveTimer : MonoBehaviour
{
    [SerializeField] SOWaveData soWaveData;
    [SerializeField] TextMeshProUGUI text;
    private void Awake() {
        text.text = "";
        EventManager<GameEvent>.Instance.AddListener(GameEvent.WaveEnd, this, TimerStart);
    }
    void TimerStart(GameEvent eventType, Component sender, object[] param) {
        int wave = (int)param[0];
        if(wave < soWaveData.WaveDatas.Count) {
            StartCoroutine(C_TimerStart(soWaveData.WaveDatas[wave].NextWaveTime));
        }
    }
    IEnumerator C_TimerStart(float time) {
        float lastTime = time;
        text.color = Color.white;
        while (lastTime > 0) {
            lastTime -= Time.deltaTime;
            text.text = string.Format("{0:0.0}", lastTime);
            if(lastTime < 10) {
                text.color = Color.red;
            }
            yield return null;
        }
        text.text = "";
    }
}
