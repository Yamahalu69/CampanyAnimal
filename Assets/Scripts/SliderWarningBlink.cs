using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SliderWarningBlink : MonoBehaviour
{
    public Slider slider;  //�ΏۃX���C�_�[
    public TMP_Text warningText;  //�X���C�_�[���̃e�L�X�g
    public string newText = "���W�Ăяo�����I";

    public float textChangeThreshold = 0.7f; //�e�L�X�g�ύX�̃X���C�_�[����
    public float blinkThreshold = 0.8f; //�_�ŊJ�n��臒l(80%)
    public float blinkInterval = 0.5f; //�_�ŊԊu

    private string originalText; //���̃e�L�X�g
    private bool isBlinking = false;
    private Coroutine blinkCoroutine;
    

    void Start()
    {
        if (warningText != null)
        {
            originalText = warningText.text; //�����e�L�X�g��ۑ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = slider.normalizedValue;
        if (slider.normalizedValue >=textChangeThreshold)
        {
            //70%�𒴂�����e�L�X�g��ύX
            if (warningText.text != newText)
            {
                warningText.text = newText;
            }
        }

        //80%�ȏ㊎��100�����œ_�ŊJ�n
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
            //80%�����܂���100%�ȏ�œ_�Œ�~&�e�L�X�g�\���Œ�
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
