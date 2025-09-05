using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] private Player prayer;

    [Header("�o�[�Ɣ�\���Ώ�")]
    [SerializeField] private Image bar;               // UI�o�[
    [SerializeField] private GameObject targetToHide; // ��\���ɂ���ΏۃI�u�W�F�N�g
    [SerializeField] private Image goal;

    [Header("����ݒ�")]
    [SerializeField] private float maxHeight = 200f;      // �㏸���E�ipx�j
    [SerializeField,Header("�o�[�̏㏸�X�s�[�h")] private float riseSpeed = 100f;      
    [SerializeField,Header("�o�[�̉��~�X�s�[�h")] private float fallSpeed = 150f;      

    [Header("�S�[������͈́ipx�j")]
    [SerializeField,Header("�S�[���͈͂̉���")] private float goalMin = 140f;  
    [SerializeField,Header("�S�[���͈͂̏��")] private float goalMax = 160f;  

    private Vector2 initialPos;//�o�[�̍ŏ��̈ʒu���L��
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

        // === �㏸���� ===
        if (Input.GetKey(KeyCode.Return) && !isFalling && !isInputLocked)
        {
            currentHeight += riseSpeed * Time.deltaTime;
            // ����ɒB�����牺�~�J�n & ���̓��b�N
            if (currentHeight >= maxHeight)
            {
                currentHeight = maxHeight;
                isFalling = true;
                isInputLocked = true;
            }
        }

        // === �������Ƃ��̃S�[������ ===
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
        
       
        // === ���~���� ===
        if (isFalling)
        {
            currentHeight -= fallSpeed * Time.deltaTime;
            if (currentHeight <= 0f)
            {
                currentHeight = 0f;
                isFalling = false;
                isInputLocked = false; // �ĂуG���^�[�L�[��t�J�n
            }
        }

        // === �o�[�ʒu�̍X�V ===
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
