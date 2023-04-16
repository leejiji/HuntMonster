using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHpbar : Hpbar {
    void Awake() {
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallItem, this, BuyWall);
        HpbarImage.gameObject.SetActive(false);
    }
    void BuyWall(ShopEvent eventType, Component sender, object param) {
        HpbarImage.gameObject.SetActive(true);
        UnitManager.Instance.GetSpawnedUnitList("Wall")[0].HpValueChangeEvent += ChangeValue;
    }
}
