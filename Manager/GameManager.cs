using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]int coin = 0;
    public int Coin { get { return coin; }set { coin = value; coin = Mathf.Max(coin, 0); if (CoinValueChange != null) CoinValueChange(coin); } }
    public Action<int> CoinValueChange;
    public Core core;
    public void Start() {
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.CoreSpawn, core, null);
    }
}
