using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager<T> : Singleton<EventManager<T>> where T : Enum
{
    public Dictionary<T, Action<T, Component, object[]>> EventDic 
        = new Dictionary<T, Action<T, Component, object[]>>();

    /// <summary>
    /// �̺�Ʈ �߰�
    /// </summary>
    /// <param name="eventType">� �̺�Ʈ�� ����Ұǰ�</param>
    /// <param name="Listener">�̺�Ʈ�� ��� ��ü �� ��ü�� ������� ��ϵ� �ݹ��� �����</param>
    /// <param name="callback">�̺�Ʈ �߻��� �����ų �ݹ��Լ�</param>
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
    /// �̺�Ʈ �߻�
    /// </summary>
    /// <param name="eventType">� �̺�Ʈ�� �߻� ��ų�ǰ�</param>
    /// <param name="sender">�̺�Ʈ�� �߻���Ų ��ü</param>
    /// <param name="param">�Ķ����</param>
    public void PostEvent(T eventType, Component sender, object[] param = null) {
        if (EventDic.ContainsKey(eventType)) {
            if(EventDic[eventType] != null)
            EventDic[eventType](eventType, sender, param);
        }
        //else
        //    Debug.Log("�̺�Ʈ ������ ����");
    }
    /// <summary>
    /// �̺�Ʈ ����
    /// </summary>
    /// <param name="eventType">� �̺�Ʈ�� ��� ���׳�</param>
    /// <param name="callback">��ϵǾ� �ִ� �ݹ� ����</param>
    public void RemoveEvent(T eventType, Action<T, Component, object[]> callback) {
        if (callback == null)
            return;
        if (EventDic.ContainsKey(eventType)) {
            EventDic[eventType] -= callback;
        }
    }
}
