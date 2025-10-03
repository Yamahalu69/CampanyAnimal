using UnityEngine;
using UnityEngine.UI;

public class MeterColorChanger : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;  // SliderのFillのImage

    public Color startColor = new Color(1f, 0.6f, 0.6f, 1f);  // 薄い赤（例）
    public Color endColor = new Color(1f, 0f, 0f, 1f);        // 濃い赤

    public float duration = 60f;  // 色が変わる時間（秒）
    private float elapsedTime = 0f;

    void Start()
    {
        slider.value = 0f;
        fillImage.color = startColor;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // スライダー値を更新（例：0→1）
            slider.value = t;

            // 色を補間
            fillImage.color = Color.Lerp(startColor, endColor, t);
        }
    }

    public void ResetMeter()
    {
        elapsedTime = 0f;
        slider.value = 0f;
        fillImage.color = startColor;
    }
}
