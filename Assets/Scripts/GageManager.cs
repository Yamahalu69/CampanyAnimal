using UnityEngine;
using UnityEngine.UI;

public class GageManager : MonoBehaviour
{
    public GameObject targetObject;  // 表示を監視するオブジェクト
    public GameObject sliderObject;  // スライダーUIのGameObject
    public Slider slider;            // スライダーコンポーネント
    public float duration = 60f;      // ゲージが満タンになる時間

    private float timeElapsed = 0f;
    private bool isRunning = false;
    void Update()
    {
        // targetObjectがアクティブならスライダーを表示してゲージを進める
        if (targetObject != null && targetObject.activeInHierarchy)
        {
            if (!isRunning)
            {
                // 表示された瞬間にリセット＆表示
                timeElapsed = 0f;
                slider.value = 0f;
                sliderObject.SetActive(true);
                isRunning = true;
            }

            // ゲージを進める
            if (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                slider.value = Mathf.Lerp(0f, 1f, timeElapsed / duration);
            }
            else
            {
                isRunning = false;
                GameManager.instance.OverWorkGameOver();
            }
        }
        else
        {
            // targetObjectが非表示ならスライダーも非表示にして停止
            if (isRunning)
            {
                sliderObject.SetActive(false);
                isRunning = false;
                slider.value = 0f;
            }
        }
    }
}