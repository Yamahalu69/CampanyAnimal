using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public RectTransform textRect;      // �������e�L�X�g��RectTransform
    public float moveDistance = 2000f;   // ���������̋����i�s�N�Z���j
    public float duration = 60f;         // �������̂ɂ����鎞�ԁi�b�j

    public RectTransform fillRect;  //�X���C�_�[��Fill��RectTransform

    private Vector2 startPos;
    private float elapsedTime = 0f;

    void Start()
    {
        startPos = textRect.anchoredPosition;  // �����ʒu��ۑ�

    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            // �E�����ɏ��X�Ɉړ�
            float newX = Mathf.Lerp(startPos.x, startPos.x + moveDistance, t);

            textRect.anchoredPosition = new Vector2(newX, startPos.y);
        }
    }
}
