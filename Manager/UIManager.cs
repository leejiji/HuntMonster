using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UIData {
    public Transform Page;
    public string PageName;
}
public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField] Transform Shop;
    [SerializeField] Transform ShopOpenButton;
    public void SetActiveShopButton(bool active) {
        ShopOpenButton.gameObject.SetActive(active);
    }
}
