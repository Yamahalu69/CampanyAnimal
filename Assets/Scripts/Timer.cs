using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float spawnInterval = 2f;  // ���ԊԊu�i�b�j

    public float timeRemaining = 300f; //5���@= 300�b
    public bool timerIsRunning = true;
    public TextMeshProUGUI timeText; //Text UI(TextMeshPro)


    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                if (timeRemaining >= spawnInterval)
                {
                    Spawn();
                    timeRemaining = 0f;
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("�^�C�}�[�I��!");
                DisplayTime(0);

                // �Q�[���I�[�o�[�V�[���ɑJ��
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1f; //�\���Y���␳

        float minutes = Mathf.FloorToInt(timeToDisplay / 60f);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60f);

        timeText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
    void Spawn()
    {
        GameManager.instance.taskManager.AddTask(Task.cleaning);
    }
}
