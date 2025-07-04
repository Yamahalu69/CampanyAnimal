using UnityEngine;

public class StockingTask : MonoBehaviour
{
    [Header("単体起動用か")]
    public bool isDebug;

    public bool isPlaying;

    [SerializeField]
    [Header("タスクの回数")]
    int task;

    [SerializeField]
    [Header("バーの速さ")]
    float speed;

    [SerializeField]
    [Header("行動不能時間")]
    float stunTime;

    [SerializeField]
    [Header("バー表示用キャンバス")]
    GameObject canvas;

    [Header("サイドバーオブジェクト")]
    public GameObject sideBarPrefab;

    [Header("サイドバーを中央からどれだけずらすか")]
    public Vector2 offset;

    private GameObject barGO;

    private RectTransform sideBar;
    private RectTransform slider;
    private RectTransform target;

    private int counter;

    private bool isPlayable;
    private float timer;
    private bool goUp;

    [SerializeField] Player player;

    void Start()
    {
        if (isDebug) StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && isPlayable)
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
                    StopTask();
                    isPlaying = false;
                    player.ssencer = false;
                    player.pl = true;
                    player.CompleateTask();
                }
                RandomMoveTarget();
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

            if (timer >= stunTime)
            {
                isPlayable = true;
            }
        }

        if (isPlayable && isPlaying)
        {
            //スライダーの移動
            if (goUp)
            {
                float y = slider.localPosition.y + speed;
                slider.localPosition = new Vector2(0, y);
                if (slider.localPosition.y >= sideBar.rect.height / 2)
                {
                    goUp = false;
                }
            }
            else
            {
                float y = slider.localPosition.y - speed;
                slider.localPosition = new Vector2 (0, y);
                if(slider.localPosition.y <= -sideBar.rect.height / 2)
                {
                    goUp = true;
                }
            }
        }
    }

    public void StartTask()
    {
        //初期化処理
        Vector2 pos = new Vector2(Screen.width / 2, Screen.height / 2) + offset;
        GameObject sideBarObject = Instantiate(sideBarPrefab, pos, Quaternion.identity);
        sideBar = sideBarObject.GetComponent<RectTransform>();
        target = sideBar.Find("TargetPoint").gameObject.GetComponent<RectTransform>();
        slider = sideBar.Find("Slider").gameObject.GetComponent<RectTransform>();
        sideBar.SetParent(canvas.GetComponent<Transform>());
        Random.InitState(System.DateTime.Now.Millisecond);
        RandomMoveTarget();

        barGO = sideBarObject;

        isPlaying = true;
        isPlayable = true;
        counter = 0;
    }

    void StopTask()
    {
        Destroy(barGO);
    }

    void RandomMoveTarget()
    {
        float y = Random.Range(sideBar.rect.height / 2 - target.rect.height / 2, -sideBar.rect.height / 2 + target.rect.height / 2);

        target.transform.localPosition = new Vector2 (0, y);
    }

    void InterruptTask()
    {
        Debug.Log("ゲームを中断しました");
        StopTask();
        isPlaying = false;
    }
}
