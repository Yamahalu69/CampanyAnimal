using UnityEngine;
using UnityEngine.UI;

public class GageManager : MonoBehaviour
{
    public GameObject targetObject;  // 表示を監視するオブジェクト
    public GameObject sliderObject;  // スライダーUIのGameObject
    public Slider slider;            // スライダーコンポーネント
    public float duration = 60f;     // ゲージが満タンになる時間

    private float timeElapsed = 0f;
    private bool isRunning = false;
    private bool wasActiveFrame = false; // 前フレームのアクティブ状態

    public TaskManager tm;

    void Update()
    {
        bool isActiveNow = targetObject != null && targetObject.activeInHierarchy;

        isActiveNow = tm.existRegisterTask;

        // targetObjectが表示された瞬間に初期化＆表示
        if (isActiveNow && !wasActiveFrame)
        {
            Debug.Log("ターゲットが表示され、ゲージをリセット");
            timeElapsed = 0f;
            slider.value = 0f;
            Canvas.ForceUpdateCanvases();
            sliderObject.SetActive(true);
            isRunning = true;
        }

        // ターゲットが非アクティブになった瞬間（非表示になったとき）
        if (!isActiveNow && wasActiveFrame)
        {

            Debug.Log("ターゲットが非表示になった：ゲージをリセット");
            sliderObject.SetActive(false);   // スライダーを非表示
            isRunning = false;
            timeElapsed = 0f;               // 時間リセット
            slider.value = 0f;        // スライダーの値リセット
            Canvas.ForceUpdateCanvases();
        }

        if (isActiveNow && isRunning)
        {
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
        
        

        // 前フレームの状態を更新
        wasActiveFrame = isActiveNow;
    }
}
