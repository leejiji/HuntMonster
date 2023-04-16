using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Text;
using TMPro;

public class SceneLoader : MonoBehaviour {
    [SerializeField] SOFadeData soFadeData;
    [SerializeField] SOFadeImageData soFadeImageData;

    [SerializeField] Canvas canvas;
    [SerializeField] Image LoadingBar;
    [SerializeField] TextMeshProUGUI ProgressText;
    Dictionary<string, FadeData> FadeDataDic = new Dictionary<string, FadeData>();
    Dictionary<string, FadeImage> FadeImageDataDic = new Dictionary<string, FadeImage>();


    FadeInOut ActiveFadeEffect;
    public void Awake() {
        Debug.Log("����");
        EventManager<SceneEvent>.Instance.AddListener(SceneEvent.SceneChangeStart, this, SceneChangeEvent);
        FadeEffectInit();
        FadeImageInit();
        LoadingBar.color = new Color(1, 1, 1, 0);
        ProgressText.color = new Color(1, 1, 1, 0);
    }

    void FadeEffectInit() {
        List<FadeData> FadeDataList = soFadeData.FadeDataList;
        for (int i = 0; i < soFadeData.FadeDataList.Count; i++) {
            string fadeEffectName = FadeDataList[i].EffectName;

            if (!FadeDataDic.ContainsKey(fadeEffectName)) {
                FadeDataDic.Add(fadeEffectName, FadeDataList[i]);
            }
            else {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(i);
                stringBuilder.Append("��° ���̵� ����Ʈ �̸��� ");
                stringBuilder.Append(fadeEffectName);
                stringBuilder.Append("�� �̹� �����մϴ�.");
                Debug.LogWarning(stringBuilder.ToString());
            }
        }
    }

    void FadeImageInit() {
        List<FadeImage> FadeImageDataList = soFadeImageData.FadeImageList;
        for (int i = 0; i < soFadeImageData.FadeImageList.Count; i++) {
            string fadeImageName = FadeImageDataList[i].ImageName;

            if (!FadeImageDataDic.ContainsKey(fadeImageName)) {
                FadeImageDataDic.Add(fadeImageName, FadeImageDataList[i]);
            }
            else {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(i);
                stringBuilder.Append("��° ���̵� �̹��� �̸��� ");
                stringBuilder.Append(fadeImageName);
                stringBuilder.Append("�� �̹� �����մϴ�.");
                Debug.LogWarning(stringBuilder.ToString());
            }
        }
    }
    public void SceneChangeEvent(SceneEvent eventType, Component sender, object[] param) {
        string sceneName = (string)param[0];
        string fadeEffectName = (string)param[1];
        float duration = (float)param[2];
        string imageName = null;
        if (param.Length >= 4)
            imageName = (string)param[3];

        if (FadeDataDic.ContainsKey(fadeEffectName)) {
            SceneChange(sceneName, fadeEffectName, duration, imageName);
        }
        else {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(fadeEffectName);
            stringBuilder.Append("�� �������� �ʾƼ� �⺻ ���̵� ����Ʈ�� ��ü�մϴ�.");
            Debug.LogWarning(stringBuilder);
            SceneChange(sceneName, soFadeData.FadeDataList[0].EffectName, duration, imageName);
        }
    }

    public void SceneChange(string sceneName, string fadeEffectName, float duration, string imageName) {
        if (FadeDataDic.ContainsKey(fadeEffectName)) {
            FadeInOut fadeEffect = FadeDataDic[fadeEffectName].FadeInOut;
            FadeInOut activeFadeEffect = Instantiate(fadeEffect, canvas.transform);
            ActiveFadeEffect = activeFadeEffect;
            ActiveFadeEffect.transform.SetAsFirstSibling();
            Image fadeImage = ActiveFadeEffect.GetComponent<Image>();

            if (imageName != null) {
                if (FadeImageDataDic.ContainsKey(imageName))
                    fadeImage.sprite = FadeImageDataDic[imageName].sprite;
            }
            Scene activeScene = SceneManager.GetSceneByName(sceneName);
            ActiveFadeEffect.FadeOut(fadeImage, duration, () => { StartCoroutine(LoadMyAsyncScene(sceneName, fadeEffectName,duration, fadeImage)); });
        }
        IEnumerator LoadMyAsyncScene(string sceneName, string fadeEffectName, float duration, Image fadeImage) {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            LoadingBar.color = new Color(1, 1, 1, 1);
            ProgressText.color = new Color(1, 1, 1, 1);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncLoad.isDone) {
                Debug.Log(asyncLoad.progress);
                LoadingBar.fillAmount = asyncLoad.progress;
                ProgressText.text = string.Format("{0:0} ", asyncLoad.progress * 100 + 10);
                yield return null;
            }
            LoadingBar.fillAmount = 1;
            ProgressText.text = "100";
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            Debug.Log(SceneManager.GetActiveScene().name);

            LoadingBar.color = new Color(1, 1, 1, 0);
            ProgressText.color = new Color(1, 1, 1, 0);
            ActiveFadeEffect.FadeIn(fadeImage, duration, ()=>Destroy(ActiveFadeEffect.gameObject));
        }
        //IEnumerator Start()
        //{
        //    InitSceneInfo();

        //    fadeCg.alpha = 1.0f;

        //    foreach(var _loadScene in loadScenes)
        //    {
        //        yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        //    }

        //    StartCoroutine(Fade(0.0f));
        //}

        //IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
        //{
        //    //�񵿱� �� �ε�
        //    yield return SceneManager.LoadSceneAsync(sceneName, mode);

        //    Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        //    SceneManager.SetActiveScene(loadedScene);
        //}

        //IEnumerator Fade(float finalAlpha)
        //{
        //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("TestRoom0"));
        //    fadeCg.blocksRaycasts = true;

        //    while (fadeImage.color.a > 0)
        //    {
        //        fadeImage.DOFade(0, 1);
        //        yield return null;
        //    }

        //    fadeCg.blocksRaycasts = false;

        //    SceneManager.UnloadSceneAsync("SceneLoader");
        //}

    }
}
