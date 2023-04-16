using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Button))]
public class StageChangeButton : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] float duration;
    [SerializeField] string FadeEffect;
    [SerializeField] string ImageName;
    Button button;
    void Awake() {
        TryGetComponent(out button);
        button.onClick.AddListener(() => { StartCoroutine(LoadMyAsyncScene()); });
    }
    IEnumerator sadasdsa() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("TestRoom1");
    }

    IEnumerator LoadMyAsyncScene() {
        Scene nowScene = SceneManager.GetActiveScene();
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        if (SceneManager.sceneCount == 1) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SceneLoader", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(nowScene);

            // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
            while (!asyncLoad.isDone) {
                yield return null;
            }
        }
        EventManager<SceneEvent>.Instance.PostEvent(SceneEvent.SceneChangeStart, null, new object[] { SceneName, FadeEffect, duration, ImageName });
        Debug.Log("페이드 이펙트");
    }
}
