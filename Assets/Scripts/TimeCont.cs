using UnityEngine;
using UnityEngine.UI;

public class TimeCont : MonoBehaviour
{
    public float totalTime = 300f; // 5���i300�b�j
    private float currentTime;
    public Text timerText; // UI�ɕ\������Text�R���|�[�l���g
    private static float deltaTime;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        // �������Ԃ�0���傫����΃J�E���g�_�E��
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            // ���Ԃ��I���������̏����i�Ⴆ�΃Q�[���I�[�o�[�Ƃ��j
            GameManager.instance.GameOver();
        }

        // ���ƕb�ɕϊ����ĕ\��
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
