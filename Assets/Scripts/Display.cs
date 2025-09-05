using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] private Player prayer;

    [Header("バーと非表示対象")]
    [SerializeField] private Image bar;               // UIバー
    [SerializeField] private GameObject targetToHide; // 非表示にする対象オブジェクト
    [SerializeField] private Image goal;

    [Header("動作設定")]
    [SerializeField] private float maxHeight = 200f;      // 上昇限界（px）
    [SerializeField,Header("バーの上昇スピード")] private float riseSpeed = 100f;      
    [SerializeField,Header("バーの下降スピード")] private float fallSpeed = 150f;      

    [Header("ゴール判定範囲（px）")]
    [SerializeField,Header("ゴール範囲の下限")] private float goalMin = 140f;  
    [SerializeField,Header("ゴール範囲の上限")] private float goalMax = 160f;  

    private Vector2 initialPos;//バーの最初の位置を記憶
    private float currentHeight = 0f;
    private bool isFalling = false;
    private bool isInputLocked = false;
    public bool crear = false;

    
    void Start()
    {
        if (bar != null)
        {
            initialPos = bar.rectTransform.anchoredPosition;
        }

        if (goal != null)
        {
            float goalheight = goalMax - goalMin;
            float centerY = initialPos.y + (goalMin+goalMax)/2;

            RectTransform goalRT = goal.rectTransform;
            goalRT.sizeDelta = new Vector2(goalRT.sizeDelta.x, goalheight);
            goalRT.anchoredPosition=new Vector2(initialPos.x, centerY);
        }
    }

    void Update()
    {
        if (bar == null || targetToHide == null) return;

        // === 上昇処理 ===
        if (Input.GetKey(KeyCode.Return) && !isFalling && !isInputLocked)
        {
            currentHeight += riseSpeed * Time.deltaTime;
            // 上限に達したら下降開始 & 入力ロック
            if (currentHeight >= maxHeight)
            {
                currentHeight = maxHeight;
                isFalling = true;
                isInputLocked = true;
            }
        }

        // === 離したときのゴール判定 ===
        if (Input.GetKeyUp(KeyCode.Return) && !isFalling && !isInputLocked)
        {
            if (currentHeight >= goalMin && currentHeight <= goalMax)
            {
                Reset();
                targetToHide.SetActive(false);
                crear = true;
                Invoke("Delay", 1.5f);

            }
            else
            {
                isFalling = true;
                isInputLocked = true;
            }
        }
        
       
        // === 下降処理 ===
        if (isFalling)
        {
            currentHeight -= fallSpeed * Time.deltaTime;
            if (currentHeight <= 0f)
            {
                currentHeight = 0f;
                isFalling = false;
                isInputLocked = false; // 再びエンターキー受付開始
            }
        }

        // === バー位置の更新 ===
        bar.rectTransform.anchoredPosition = new Vector2(initialPos.x, initialPos.y + currentHeight);
    }
    void Delay()
    {
        
        prayer.CompleateTask();
        prayer.dsencer = false;
        prayer.pl = true; 
    }

    void EnterDelay()
    {
        isInputLocked = true;
    }
    public void Reset()
    {
        currentHeight = 0f;
        bar.rectTransform.anchoredPosition = initialPos;
        isFalling = false;
        isInputLocked = false;
    }
}
