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
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        if (SceneManager.sceneCount == 1) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SceneLoader", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(nowScene);

            // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
            while (!asyncLoad.isDone) {
                yield return null;
            }
        }
        EventManager<SceneEvent>.Instance.PostEvent(SceneEvent.SceneChangeStart, null, new object[] { SceneName, FadeEffect, duration, ImageName });
        Debug.Log("���̵� ����Ʈ");
    }
}
