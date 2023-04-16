using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpbar : Hpbar
{
    [SerializeField] Unit unit;
    [SerializeField] Vector2 OffSet = Vector3.zero;
    Vector2 BackGroundPos;
    private void Start() {
        HpbarSetting(unit, OffSet);
        if(BackGround != null) {
            StartCoroutine(MoveImage(BackGround));
        }
        else {
            StartCoroutine(MoveImage(HpbarImage));
        }
    }
    IEnumerator MoveImage(Image image) {
        while (true) {
            image.rectTransform.position = Camera.main.WorldToScreenPoint(unit.transform.position) + (Vector3)OffSet;
            yield return null;
        }
    }
    public void HpbarSetting(Unit unit, Vector2 offSet) {
        this.unit = unit;
        unit.HpValueChangeEvent += ChangeValue;
        OffSet = offSet;
        ChangeValue(unit);
    }
}
