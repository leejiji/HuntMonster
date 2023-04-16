using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop : MonoBehaviour{
    [SerializeField] TextMeshProUGUI CoinText;

    private void Awake() {
        GameManager.Instance.CoinValueChange += CoinTextChange;
    }
    void CoinTextChange(int coin) {
        CoinText.text = coin.ToString();
    }
    private void OnEnable() {
        CoinText.text = GameManager.Instance.Coin.ToString();
    }
    public void OnClickGoldUp() {
        GameManager.Instance.Coin += 1000;
    }
}
