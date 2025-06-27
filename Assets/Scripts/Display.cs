using UnityEngine;
using UnityEngine.UI;

public class PressToRiseAndFallBar : MonoBehaviour
{
    [Header("バーと非表示対象")]
    [SerializeField] private Image bar;               // UIバー
    [SerializeField] private GameObject targetToHide; // 非表示にする対象オブジェクト

    [Header("動作設定")]
    [SerializeField] private float maxHeight = 200f;      // 上昇限界（px）
    [SerializeField] private float riseSpeed = 100f;      // 上昇速度（px/sec）
    [SerializeField] private float fallSpeed = 150f;      // 下降速度（px/sec）
    [SerializeField] private float triggerHeight = 150f;  // 非表示判定の高さ

    private Vector2 initialPos;
    private float currentHeight = 0f;
    private bool isFalling = false;
    private bool hasHidden = false;

    void Start()
    {
        if (bar != null)
        {
            initialPos = bar.rectTransform.anchoredPosition;
        }
    }

    void Update()
    {
        if (bar == null || targetToHide == null || hasHidden) return;

        // エンター長押しで上昇（下降中は無視）
        if (Input.GetKey(KeyCode.Return) && !isFalling)
        {
            currentHeight += riseSpeed * Time.deltaTime;
            currentHeight = Mathf.Clamp(currentHeight, 0f, maxHeight);
        }

        // エンターキー離したら判定（下降フラグ設定）
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (currentHeight >= triggerHeight)
            {
                targetToHide.SetActive(false);
                hasHidden = true;
            }
            else
            {
                isFalling = true;
            }
        }

        // バー下降中
        if (isFalling)
        {
            currentHeight -= fallSpeed * Time.deltaTime;
            if (currentHeight <= 0f)
            {
                currentHeight = 0f;
                isFalling = false; // 降りきったら停止
            }
        }

        // バー位置更新
        bar.rectTransform.anchoredPosition = new Vector2(initialPos.x, initialPos.y + currentHeight);
    }
}
