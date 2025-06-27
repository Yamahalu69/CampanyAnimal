using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SliderWarningBlink : MonoBehaviour
{
    public Slider slider;  //対象スライダー
    public TMP_Text warningText;  //スライダー横のテキスト
    public string newText = "レジ呼び出し中！";

    public float textChangeThreshold = 0.7f; //テキスト変更のスライダー割合
    public float blinkThreshold = 0.8f; //点滅開始の閾値(80%)
    public float blinkInterval = 0.5f; //点滅間隔

    private string originalText; //元のテキスト
    private bool isBlinking = false;
    private Coroutine blinkCoroutine;
    

    void Start()
    {
        if (warningText != null)
        {
            originalText = warningText.text; //初期テキストを保存
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = slider.normalizedValue;
        if (slider.normalizedValue >=textChangeThreshold)
        {
            //70%を超えたらテキストを変更
            if (warningText.text != newText)
            {
                warningText.text = newText;
            }
        }

        //80%以上且つ100未満で点滅開始
        if (value >= blinkThreshold && value < 1.0f)
        {
            if (!isBlinking)
            {
                blinkCoroutine = StartCoroutine(BlinkText());
                isBlinking = true;
            }
        }
        else
        {
            //80%未満または100%以上で点滅停止&テキスト表示固定
            if (isBlinking)
            {
                StopCoroutine(blinkCoroutine);
                warningText.enabled = true;
                isBlinking = false;
            }
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            warningText.enabled = !warningText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
