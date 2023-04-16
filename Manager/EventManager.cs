using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager<T> : Singleton<EventManager<T>> where T : Enum
{
    public Dictionary<T, Action<T, Component, object[]>> EventDic 
        = new Dictionary<T, Action<T, Component, object[]>>();

    /// <summary>
    /// 이벤트 추가
    /// </summary>
    /// <param name="eventType">어떤 이벤트에 등록할건가</param>
    /// <param name="Listener">이벤트를 듣는 객체 이 객체가 사라지면 등록된 콜백이 사라짐</param>
    /// <param name="callback">이벤트 발생시 실행시킬 콜백함수</param>
    public void AddListener(T eventType, Component Listener, Action<T, Component, object[]> callback) {
        if (callback == null)
            return;

        if (!EventDic.ContainsKey(eventType)) {
            Action<T, Component, object> dic = null;
            EventDic.Add(eventType, dic);
        }

        Action<T, Component, object[]> action = null;
        action = (_eventType, _ob, param) => {
            if (Listener == null || Listener.gameObject.activeSelf == false) { RemoveEvent(eventType, action); return; }
            callback(_eventType, _ob, param);
        };
        EventDic[eventType] += action;
    }
    /// <summary>
    /// 이벤트 발생
    /// </summary>
    /// <param name="eventType">어떤 이벤트를 발생 시킬건가</param>
    /// <param name="sender">이벤트를 발생시킨 객체</param>
    /// <param name="param">파라미터</param>
    public void PostEvent(T eventType, Component sender, object[] param = null) {
        if (EventDic.ContainsKey(eventType)) {
            if(EventDic[eventType] != null)
            EventDic[eventType](eventType, sender, param);
        }
        //else
        //    Debug.Log("이벤트 리스너 없따");
    }
    /// <summary>
    /// 이벤트 삭제
    /// </summary>
    /// <param name="eventType">어떤 이벤트에 등록 시켰나</param>
    /// <param name="callback">등록되어 있는 콜백 삭제</param>
    public void RemoveEvent(T eventType, Action<T, Component, object[]> callback) {
        if (callback == null)
            return;
        if (EventDic.ContainsKey(eventType)) {
            EventDic[eventType] -= callback;
        }
    }
}
