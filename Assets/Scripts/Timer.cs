using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 300f; //5分　= 300秒
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
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("タイマー終了!");
                DisplayTime(0);

                // ゲームオーバーシーンに遷移
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1f; //表示ズレ補正

        float minutes = Mathf.FloorToInt(timeToDisplay / 60f);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60f);

        timeText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
