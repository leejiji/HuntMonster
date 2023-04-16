using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealItem :ShopItem {

    protected override void BuyItem() {
        EventManager<ShopEvent>.Instance.PostEvent(ShopEvent.BuyWallHealItem, this, null);
    }

    protected override bool BuyItemCondition() {
        if (UnitManager.Instance.GetSpawnedUnitList("Wall").Count > 0) {
            Unit wall = UnitManager.Instance.GetSpawnedUnitList("Wall")[0];
            if (wall != null && wall.HP > 0) {
                return wall.HP >= wall.SOUnitData.MaxHP ? false : true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
