using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderWithText : MonoBehaviour
{
    public Slider slider;
    public TMP_Text fillText;
    public float duration = 60f; //�X���C�_�[��60�b�ōő�܂Ői�� 
    public float showThreshold = 0.4f; //�X���C�_�[��40%�𒴂�����\��
    public float margin = 10f; //�e�L�X�g�ƉE�[�̗]��

    private RectTransform fillRect;
    private float timer = 0f;

    private bool isRunning = true; // �X���C�_�[�i�s�����ǂ���


    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        fillRect = slider.fillRect;
        fillText.gameObject.SetActive(false);�@//�ŏ��͔�\��
    }

    void Update()
    {
        // �X���C�_�[��60�b�Ői�߂�
        if (isRunning && slider.value < slider.maxValue)
        {
            timer += Time.deltaTime;
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, timer / duration);
        }

        // Fill�̕��ƃe�L�X�g�����擾
        float fillWidth = fillRect.rect.width;
        float textWidth = fillText.preferredWidth;

        // �e�L�X�g�����܂�������m�F
        bool canShowText = slider.normalizedValue >= showThreshold &&
                           fillWidth >= textWidth + margin;

        if (canShowText)
        {
            if (!fillText.gameObject.activeSelf)
                fillText.gameObject.SetActive(true);

            // Fill�̍��[����E��text��z�u�iPivot = ���̑O��j
            Vector2 anchoredPos = fillText.rectTransform.anchoredPosition;
            anchoredPos.x = fillWidth - textWidth - margin;
            fillText.rectTransform.anchoredPosition = anchoredPos;

            fillText.text = "���q�l���[�^�[";


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
        isRunning = false; // ���Z�b�g��͐i�s��~
    }
}
