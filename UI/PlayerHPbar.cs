using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerHPbar : Hpbar
{
    void Awake()
    {
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.ChangeHp,this, PlayerhpChange);
    }
    void PlayerhpChange(PlayerEvent eventType, Component sender, object param) {
        if(sender is Player) {
            Player player = (Player)sender;
            ChangeValue(player);
        }
    }
}
