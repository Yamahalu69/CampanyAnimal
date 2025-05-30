using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageManager : MonoBehaviour
{
    public Slider slider;
    public float startValue = 0.4f;   // 初期値40%
    public float endValue = 1.0f;     // 最終値100%
    public float duration = 60f;      // 60秒で満タンに

    private float elapsedTime = 0f;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = startValue;   // ここで初期値セット
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            slider.value = Mathf.Lerp(startValue, endValue, t);
        }
    }
}
