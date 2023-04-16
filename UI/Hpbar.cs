using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Hpbar : MonoBehaviour {
    [SerializeField] protected Image HpbarImage;
    [SerializeField] protected Image BackGround;
    float Value = 1;
    public void ChangeValue(Unit unit) {
        Value = (float)unit.HP / (float)unit.SOUnitData.MaxHP;
        HpbarImage.DOKill();
        HpbarImage.DOFillAmount(Value, 1);
    }
}
