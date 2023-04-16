using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicitItem : ShopItem {
    protected override void BuyItem() {
        EventManager<ShopEvent>.Instance.PostEvent(ShopEvent.BuyMedicitItem, this, null);
    }

    // �÷��̾� HP�� �ִ밡 �ƴϸ� ���� ����
    protected override bool BuyItemCondition() {
        List<Unit> PlayerList = UnitManager.Instance.GetSpawnedUnitList("Player");
        if(PlayerList != null) {
            return PlayerList[0].HP == PlayerList[0].SOUnitData.MaxHP || PlayerList[0].HP <= 0 ? false : true;
        }
        else {
            Debug.LogWarning("�÷��̾ �����ϴ�");
            return false;
        }
    }
}
