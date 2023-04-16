using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class AnimEvent : MonoBehaviour
{
    public UnityEvent UnitEvent;
    public UnityEvent UnitEvent2;

    public void Invoke() {
        UnitEvent.Invoke();
    }
    public void Invoke2() {
        UnitEvent2.Invoke();
    }
}
