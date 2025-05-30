using UnityEngine;

public class StockingTask : MonoBehaviour
{
    public bool isPlaying;

    [SerializeField]
    [Tooltip("タスクの回数")]
    int task;

    [SerializeField]
    [Tooltip("バーの速さ")]
    float speed;

    [SerializeField]
    [Tooltip("行動不能時間")]
    float stunTime;

    [SerializeField]
    [Tooltip("バー表示用キャンバス")]
    GameObject canvas;

    public GameObject sideBarPrefab;

    private RectTransform sideBar;
    private RectTransform slider;
    private RectTransform target;

    private int counter;

    private bool isPlayable;
    private float timer;

    void Start()
    {
        StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.Escape) && isPlayable)
        {
            InterruptTask();
        }

        if (Input.GetKeyDown(KeyCode.Return) && isPlayable)
        {
            //バーが重なっているか判定
            float upLimit = target.position.y + target.rect.height / 2f;
            float downLimit = target.position.y - target.rect.height / 2f;
            
            if (slider.position.y <= upLimit && slider.position.y >= downLimit)
            {
                //範囲内
                counter++;
                if (counter >= task)
                {
                    //タスク完了
                    Debug.Log("タスク完了");
                    isPlaying = false;
                }
            }
            else
            {
                //範囲外

                //行動不能
                isPlayable = false;
                timer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isPlayable && isPlaying)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= stunTime)
            {
                isPlayable = true;
            }
        }

        if (isPlayable && isPlaying)
        {
            //スライダーの移動
            float y = slider.localPosition.y + speed;
            slider.localPosition = new Vector2(0, y);
            if (slider.localPosition.y > sideBar.rect.height / 2)
            {
                slider.localPosition = new Vector2(0, -sideBar.rect.height / 2);
            }
        }
    }

    public void StartTask()
    {
        GameObject sideBarObject = Instantiate(sideBarPrefab, new(1560f, 540f), Quaternion.identity);
        sideBar = sideBarObject.GetComponent<RectTransform>();
        target = sideBar.Find("TargetPoint").gameObject.GetComponent<RectTransform>();
        slider = sideBar.Find("Slider").gameObject.GetComponent<RectTransform>();
        sideBar.SetParent(canvas.GetComponent<Transform>());

        isPlaying = true;
        isPlayable = true;
        counter = 0;
    }

    void InterruptTask()
    {
        Debug.Log("ゲームを中断しました");
        isPlaying = false;
    }
}
