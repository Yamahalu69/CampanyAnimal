using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class TaskTextAnimation : MonoBehaviour
{
    public Slider slider;
    public Text textC;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EraseTextAnim();
        }
    }

    public void EraseTextAnim()
    {
        textC = GetComponent<Text>();
    }

    IEnumerator Anim()
    {
        yield return DOTween.To(() => slider.value, (x) => slider.value = x, 1, 0.5f).WaitForCompletion();
    }
}
