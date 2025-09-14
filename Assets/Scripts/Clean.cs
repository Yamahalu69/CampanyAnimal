using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clean : MonoBehaviour
{
    [Header("Player Script")]
    [SerializeField] private Player prayer;

    [Header("UI")]
    [SerializeField] private Slider bar;
    [SerializeField] private GameObject cleantask;
    [SerializeField] private Text enterText;

    [Header("Input Settings")]
    [SerializeField, Tooltip("�ő���͉�")] private int maxCount = 10;
    private int currentCount = 0;

    private bool taskCompleted = false;

    // �O�t���[����cleantask�̕\����Ԃ��L�^
    private bool previousCleantaskActive = true;

    private void Start()
    {
        ResetProgress();
        cleantask?.SetActive(true);
        previousCleantaskActive = cleantask?.activeSelf ?? false;
    }

    private void Update()
    {
        // Enter�L�[�Ői�s
        if (!taskCompleted && Input.GetKeyDown(KeyCode.Return))
        {
            currentCount++;
            UpdateSlider();

            // �F��ύX�i�����ꂽ�u�ԁj
            enterText.color = new Color(1f, 0f, 0f, 0.5f);

            if (currentCount >= maxCount)
            {
                CompleteTask();
            }
        }

        // �L�[�𗣂�����F��߂�
        if (Input.GetKeyUp(KeyCode.Return))
        {
            enterText.color = new Color(0f, 0f, 0f, 1f);
        }

        // Canvas�icleantask�j����\���ɂȂ����u�ԂɃ��Z�b�g
        if (cleantask != null)
        {
            if (previousCleantaskActive && !cleantask.activeSelf)
            {
                ResetProgress();
            }

            previousCleantaskActive = cleantask.activeSelf;
        }
    }

    private void UpdateSlider()
    {
        if (bar != null && maxCount > 0)
        {
            float progress = (float)currentCount / maxCount;
            bar.value = Mathf.Clamp01(progress);
        }
    }

    private void ResetProgress()
    {
        currentCount = 0;
        taskCompleted = false;

        if (bar != null)
        {
            bar.minValue = 0f;
            bar.maxValue = 1f;
            bar.value = 0f;
        }

        enterText.color = new Color(0f, 0f, 0f, 1f);
    }

    private void CompleteTask()
    {
        taskCompleted = true;

        if (cleantask != null)
        {
            cleantask.SetActive(false);
        }

        if (prayer != null)
        {
            prayer.CompleateTask();
            prayer.csencer = false;
            prayer.pl = true;
        }
    }
}
