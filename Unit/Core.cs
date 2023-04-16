using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Unit {
    [SerializeField] SOUnit soUnit = null;
    public override SOUnit SOUnitData => soUnit;
    public bool Invincivility;
    public void Start() {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        DieEvent += (unit) => { EventManager<GameEvent>.Instance.PostEvent(GameEvent.GameEnd, this, null); };
    }
    public override void Damaged(int damage) {
        if(!Invincivility)
        base.Damaged(damage);
    }
}
