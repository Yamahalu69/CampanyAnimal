using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] Image bar;//�ړ�����o�[
    [SerializeField] Image goal;//�~�߂�ʒu
    [SerializeField] private float movedistance = 200f;//����
    [SerializeField, Header("�o�[�̏㏸�X�s�[�h")] private float movesp = 2f;//�㏸�X�s�[�h
    [SerializeField, Header("�o�[�̉��~�X�s�[�h")] private float downsp = 2f;//���H�X�s�[�h

    private Vector2 farstpos;
    private float curr = 0f;
    private bool enterlock = false;//���~�����ǂ���
    private bool islock = false;//���͂����b�N����Ă��邩
    private bool clear = false;//�S�[���Ƀo�[�����邩�ǂ���

    private void Start()
    {
        if (bar != null)
        {
            farstpos = bar.rectTransform.anchoredPosition;
            bar.rectTransform.anchoredPosition = farstpos;
        }
    }

    private bool Ingoal()
    {
        RectTransform goalrt = goal.rectTransform;
        RectTransform barrt = bar.rectTransform;

        float bary = barrt.anchoredPosition.y;
        float goaly = goalrt.anchoredPosition.y;
        float goalheight = goalrt.rect.height;

        float goaltop = goaly + goalheight / 2f;
        float goalbottom = goaly - goalheight / 2f;

        return bary >= goalbottom && bary <= goaltop;
    }

    private void Update()
    {
        if (clear) return;

        if(!enterlock &&islock&&Ingoal())
        {
            clear = true;
            this.gameObject.SetActive(false);
        }
        //�o�[�㏸
        if (!islock && !enterlock && Input.GetKey(KeyCode.Return))
        {
            curr += movesp * Time.deltaTime;

            if (curr >= movedistance)
            {
                curr = movedistance;
                enterlock = true;
                islock = true;
            }
        }

        //Enter�L�[�𗣂��Ɖ��~�J�n
        if (!islock && !Input.GetKey(KeyCode.Return) && curr > 0f)
        {
            enterlock = true;
            islock = true;
        }

        //�o�[�����~��
        if (enterlock)
        {
            curr -= downsp * Time.deltaTime;
            if (curr <= 0f)
            {
                curr = 0f;
                enterlock = false;
                islock = false;
            }
        }

        //�o�[�̈ʒu���X�V
        if (bar != null)
        {
            bar.rectTransform.anchoredPosition = new Vector2(farstpos.x, farstpos.y + curr);
        }

        //�o�[���S�[���ɓ��B���Ă��邩�ǂ���
        if (Ingoal() && !enterlock && Input.GetKeyUp(KeyCode.Return))
        {
            clear = true;
            this.gameObject.SetActive(false);
        }
    }



}