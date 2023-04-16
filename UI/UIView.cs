using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[Serializable]
public struct UIViewPage {
    public GameObject PageObject;
    public string PageName;
}
public class UIView : MonoBehaviour
{
    [SerializeField] bool FirstPageOpen = true;
    [SerializeField] protected List<UIViewPage> PageList = new List<UIViewPage>();
    protected Stack<UIViewPage> PageStack = new Stack<UIViewPage>();
    Dictionary<string, UIViewPage> PageDic = new Dictionary<string, UIViewPage>();
    UIViewPage ActivePage;
    void Start()
    {
        foreach(UIViewPage page in PageList) {
            if (!PageDic.ContainsKey(page.PageName))
                PageDic.Add(page.PageName, page);
        }
        if(FirstPageOpen)
        if (PageList.Count > 0) {
            Push(PageList[0].PageName);
        }
    }
    public virtual void Push(string pageName) {
        if (PageDic.ContainsKey(pageName)) {
            UIViewPage page = PageDic[pageName];
            if (!PageStack.Contains(page)) {
                Hide();
                ActivePage = page;
                PageStack.Push(page);
                Show();
            }
        } else
            Debug.LogWarning(pageName + " 해당 페이지는 없습니다");
    }
    public virtual void Pop() {
        Hide();
        PageStack.Pop();
        if (PageStack.Count > 0) {
            UIViewPage page = PageStack.Peek();
            ActivePage = page;
            Show();
        }
    }
    void Show() {
        if (ActivePage.PageObject != null)
            ActivePage.PageObject.gameObject.SetActive(true);
    }
    void Hide() {
        if(ActivePage.PageObject != null)
        ActivePage.PageObject.gameObject.SetActive(false);
    }
    //public void PlayEffect(string effectName) {
    //    SoundManager.Instance.PlayEffect(effectName);
    //}
}
