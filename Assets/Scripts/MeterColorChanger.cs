using UnityEngine;
using UnityEngine.UI;

public class MeterColorChanger : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;  // Slider��Fill��Image

    public Color startColor = new Color(1f, 0.6f, 0.6f, 1f);  // �����ԁi��j
    public Color endColor = new Color(1f, 0f, 0f, 1f);        // �Z����

    public float duration = 60f;  // �F���ς�鎞�ԁi�b�j
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

            // �X���C�_�[�l���X�V�i��F0��1�j
            slider.value = t;

            // �F����
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
