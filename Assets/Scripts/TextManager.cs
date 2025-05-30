using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public RectTransform textRect;      // 動かすテキストのRectTransform
    public float moveDistance = 2000f;   // 動かす横の距離（ピクセル）
    public float duration = 60f;         // 動かすのにかける時間（秒）

    public RectTransform fillRect;  //スライダーのFillのRectTransform

    private Vector2 startPos;
    private float elapsedTime = 0f;

    void Start()
    {
        startPos = textRect.anchoredPosition;  // 初期位置を保存

    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            // 右方向に徐々に移動
            float newX = Mathf.Lerp(startPos.x, startPos.x + moveDistance, t);

            textRect.anchoredPosition = new Vector2(newX, startPos.y);
        }
    }
}
