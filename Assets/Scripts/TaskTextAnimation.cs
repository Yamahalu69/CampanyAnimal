using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class TaskTextAnimation : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Text textC;

    public float lineAnimTime;
    public float fadeAnimTime;

    public IEnumerator EraseTextAnim()
    {
        textC = GetComponent<Text>();
        yield return StartCoroutine(Anim());
        Destroy(gameObject);
    }

    IEnumerator Anim()
    {
        yield return DOTween.To(() => slider.value, (x) => slider.value = x, 1, lineAnimTime).WaitForCompletion();
        var textColor = textC.color;
        textColor.a = 0;
        textC.DOColor(textColor, fadeAnimTime);
        yield return fill.DOFade(0f, fadeAnimTime).WaitForCompletion();
    }
}
