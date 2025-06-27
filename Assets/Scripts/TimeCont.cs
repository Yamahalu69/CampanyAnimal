using UnityEngine;
using UnityEngine.UI;

public class TimeCont : MonoBehaviour
{
    public float totalTime = 300f; // 5分（300秒）
    private float currentTime;
    public Text timerText; // UIに表示するTextコンポーネント
    private static float deltaTime;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        // 制限時間が0より大きければカウントダウン
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            // 時間が終了した時の処理（例えばゲームオーバーとか）
            GameManager.instance.GameOver();
        }

        // 分と秒に変換して表示
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
