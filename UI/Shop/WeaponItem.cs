using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponItem : ShopItem
{
    [SerializeField] TextMeshProUGUI WeaponDataText;
    [SerializeField] TextMeshProUGUI WeaponUpgradeText;
    [SerializeField] int UpgradeValue = 5;
    [SerializeField] Character_Weapon Weapon;
    [SerializeField] Weapon_Unit WeaponData;

    protected override void Awake() {
        base.Awake();
    }
    private void Start() {
        Unit Player = UnitManager.Instance.GetSpawnedUnitList("Player")[0];
        Weapon = Player.GetComponent<Character_Weapon>();
        WeaponData = Weapon.WeaponData;
        TextUpdate();
    }
    protected override void BuyItem() {
        Weapon.Weapon_Upgrade(UpgradeValue);
        TextUpdate();
    }
    void TextUpdate() {
        WeaponDataText.text ="Now Power : " + WeaponData.Power.ToString();
        WeaponUpgradeText.text = "Upgrade Power : " + (WeaponData.Power + UpgradeValue).ToString();
    }
    protected override bool BuyItemCondition() {
        return true;
    }
}
