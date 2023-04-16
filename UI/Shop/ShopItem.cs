using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
public abstract class ShopItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] GameObject AbleToPurchase;
    [SerializeField] GameObject UnableToPurchase;
    [SerializeField] int Cost;
    [SerializeField] int IncreaseCost;
    [SerializeField] int NumberOfBuy;
    [SerializeField] protected bool isBuyContinue;
    bool isBuyItem = true;
    public Button BuyButton;

    [Inject] IPlaySound m_PlaySound;
    protected virtual void Awake() {
        BuyButton.onClick.AddListener(OnClickBuyButton);
        CoinText.text = Cost.ToString();
        AbleToPurchase.SetActive(true);
        UnableToPurchase.SetActive(false);
    }
    void OnClickBuyButton() {
        if (isBuyItem && BuyItemCondition()) {
            if (Cost <= GameManager.Instance.Coin) {
                m_PlaySound.PlayOneShot(SoundType.SFX, "BuyButton");
                BuyItem();
                GameManager.Instance.Coin -= Cost;
                Cost += IncreaseCost;
                if (!isBuyContinue) {
                    NumberOfBuy--;
                    if (NumberOfBuy <= 0) {
                        isBuyItem = false;
                        BuyEnd();
                    }
                }
            }
            CoinText.text = Cost.ToString();
        }
    }
    void BuyEnd() {
        AbleToPurchase.SetActive(false);
        UnableToPurchase.SetActive(true);
    }
    /// <summary>
    /// 아이템 구매 조건
    /// </summary>
    /// <returns></returns>
    protected abstract bool BuyItemCondition();
    /// <summary>
    /// 아이템 구매시 일어나는 일
    /// </summary>
    protected abstract void BuyItem();
}
