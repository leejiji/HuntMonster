using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;
public class StartTimer : MonoBehaviour
{
    [SerializeField] GameObject BackGround;
    [SerializeField] Image TimeImage;
    [SerializeField] Image TimerEndImage;
    [SerializeField] Image PlayerHead;
    [SerializeField] Sprite[] sprites;
    [SerializeField] int Time;
    [Inject] IPlaySound m_PlaySound;
    void Start()
    {
        StartCoroutine(C_StartTime(Time));
    }
    IEnumerator C_StartTime(int time) {
        TimeImage.gameObject.SetActive(true);
        BackGround.SetActive(true);
        PlayerHead.gameObject.SetActive(true);
        TimerEndImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        TimeImage.color = new Color(1, 1, 1, 1);
        Vector3 scale = TimeImage.rectTransform.localScale;
        m_PlaySound.PlayOneShot(SoundType.SFX, "CountDown");
        while (time > 0) {
            TimeImage.sprite = sprites[time - 1];
            TimeImage.rectTransform.localScale = scale * 1.5f;
            TimeImage.rectTransform.DOScale(scale.y, 1);
            time--;
            yield return new WaitForSeconds(1f);
        }
        TimeImage.gameObject.SetActive(false);
        BackGround.SetActive(false);
        TimerEndImage.gameObject.SetActive(true);

        float moveX = PlayerHead.rectTransform.rect.width;
        PlayerHead.transform.DOMoveX(PlayerHead.transform.position.x - moveX * 1.5f, 1).OnComplete(() => { PlayerHead.gameObject.SetActive(false); }).SetEase(Ease.OutQuint); ;
        TimerEndImage.rectTransform.localScale = scale;
        TimerEndImage.rectTransform.DOScale(scale.y, 1.001f).OnComplete(() => {  TimerEndImage.gameObject.SetActive(false); }).SetEase(Ease.OutQuint);
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.TimerEnd, this, null);
    }
}
