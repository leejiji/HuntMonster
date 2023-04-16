using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallItem : ShopItem
{
    bool buyWall = false;
    protected override void BuyItem() {
        EventManager<ShopEvent>.Instance.PostEvent(ShopEvent.BuyWallItem, this, null);
        buyWall = true;
        Debug.Log("º® ±¸¸Å");
    }

    protected override bool BuyItemCondition() {
        if (UnitManager.Instance.GetSpawnedUnitList("Wall").Count > 0) {
            Unit wall = UnitManager.Instance.GetSpawnedUnitList("Wall")[0];
            if (wall.HP <= 0 || !buyWall) {
                return true;
            }
        }
        else {
            return true;
        }
        return false;
    }
}
