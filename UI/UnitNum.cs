using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnitNum : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UnitText;
    int num = 0;
    private void Awake() {
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Spawn, this,TextUpdate);
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Die, this, DieTextUpdate);
    }
    void TextUpdate(MonsterEvent type, Component sender, object param) {
        num++;
        UnitText.text = "UnitNum : "+  num.ToString();
    }
    void DieTextUpdate(MonsterEvent type, Component sender, object param) {
        num--;
        UnitText.text = "UnitNum : " + num.ToString();
    }
}
