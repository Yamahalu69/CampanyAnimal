using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderWithText : MonoBehaviour
{
    public Slider slider;
    public TMP_Text fillText;
    public float duration = 60f; //スライダーが60秒で最大まで進む 
    public float showThreshold = 0.4f; //スライダーが40%を超えたら表示
    public float margin = 10f; //テキストと右端の余白

    private RectTransform fillRect;
    private float timer = 0f;

    private bool isRunning = true; // スライダー進行中かどうか


    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        fillRect = slider.fillRect;
        fillText.gameObject.SetActive(false);　//最初は非表示
    }

    void Update()
    {
        // スライダーを60秒で進める
        if (isRunning && slider.value < slider.maxValue)
        {
            timer += Time.deltaTime;
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, timer / duration);
        }

        // Fillの幅とテキスト幅を取得
        float fillWidth = fillRect.rect.width;
        float textWidth = fillText.preferredWidth;

        // テキストが収まる条件を確認
        bool canShowText = slider.normalizedValue >= showThreshold &&
                           fillWidth >= textWidth + margin;

        if (canShowText)
        {
            if (!fillText.gameObject.activeSelf)
                fillText.gameObject.SetActive(true);

            // Fillの左端から右へtextを配置（Pivot = 左の前提）
            Vector2 anchoredPos = fillText.rectTransform.anchoredPosition;
            anchoredPos.x = fillWidth - textWidth - margin;
            fillText.rectTransform.anchoredPosition = anchoredPos;

            fillText.text = "お客様メーター";


        }
        else
        {
            if (fillText.gameObject.activeSelf)
                fillText.gameObject.SetActive(false);
        }
    }
       
    public void StartSlider()
    {
        isRunning = true;
    }

    public void ResetSlider()
    {
        timer = 0;
        slider.value = 0f;
        fillText.gameObject.SetActive(false);
        isRunning = false; // リセット後は進行停止
    }
}
