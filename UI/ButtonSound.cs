using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    Button button;
    [SerializeField] SoundType Type;
    [SerializeField] string Clip;
    [Inject] IPlaySound play;
    void Start() {
        TryGetComponent(out button);
        button.onClick.AddListener(() => { play.PlaySound(Type, Clip); });
    }
}
