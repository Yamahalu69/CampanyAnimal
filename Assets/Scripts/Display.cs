using UnityEngine;
using UnityEngine.UI;

public class PressToRiseAndFallBar : MonoBehaviour
{
    [Header("�o�[�Ɣ�\���Ώ�")]
    [SerializeField] private Image bar;               // UI�o�[
    [SerializeField] private GameObject targetToHide; // ��\���ɂ���ΏۃI�u�W�F�N�g

    [Header("����ݒ�")]
    [SerializeField] private float maxHeight = 200f;      // �㏸���E�ipx�j
    [SerializeField] private float riseSpeed = 100f;      // �㏸���x�ipx/sec�j
    [SerializeField] private float fallSpeed = 150f;      // ���~���x�ipx/sec�j
    [SerializeField] private float triggerHeight = 150f;  // ��\������̍���

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

        // �G���^�[�������ŏ㏸�i���~���͖����j
        if (Input.GetKey(KeyCode.Return) && !isFalling)
        {
            currentHeight += riseSpeed * Time.deltaTime;
            currentHeight = Mathf.Clamp(currentHeight, 0f, maxHeight);
        }

        // �G���^�[�L�[�������画��i���~�t���O�ݒ�j
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

        // �o�[���~��
        if (isFalling)
        {
            currentHeight -= fallSpeed * Time.deltaTime;
            if (currentHeight <= 0f)
            {
                currentHeight = 0f;
                isFalling = false; // �~�肫�������~
            }
        }

        // �o�[�ʒu�X�V
        bar.rectTransform.anchoredPosition = new Vector2(initialPos.x, initialPos.y + currentHeight);
    }
}
