using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterTask : MonoBehaviour
{
    public bool isPlaying;

    List<KeyCode> arrowsList = new List<KeyCode>();

    public int arrowsAmount;

    private int counter = 0;

    //���\���p�L�����o�X
    public GameObject canvas;

    public GameObject arrowPrefab;

    public List<SwitchSprite> arrowSprites = new List<SwitchSprite>();

    void Start()
    {
        StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InterruptTask();
        }

        // ���͔���
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (Input.GetKeyDown(arrowsList[counter]))
            {
                //����������
                counter++;
                if (counter == arrowsList.Count)
                {
                    //�^�X�N����
                    Debug.Log("�^�X�N����");
                }

                //�\���X�V
                ViewSprite();
            }
            else
            {
                //�Ԉ��������
                StartTask();
            }
        }
    }

    public void StartTask()
    {
        //����������
        Random.InitState(System.DateTime.Now.Millisecond);
        isPlaying = true;
        counter = 0;
        arrowsList.Clear();

        for (int i = 0; i < arrowsAmount; i++)
        {
            int r = Random.Range(1, 5);
            Debug.Log(r);
            switch (r)
            {
                case 1:
                    arrowsList.Add(KeyCode.UpArrow);
                    break;
                case 2:
                    arrowsList.Add(KeyCode.DownArrow);
                    break;
                case 3:
                    arrowsList.Add(KeyCode.RightArrow);
                    break;
                case 4:
                    arrowsList.Add(KeyCode.LeftArrow);
                    break;
            }
        }

        if (arrowSprites.Count == 0)
        {
            //���\���p�I�u�W�F�N�g����
            for (int i = 0; i < 4; i++)
            {
                Vector3 pos = new(1510f - 450 * i, 200f, 0f);

                GameObject arrow = Instantiate(arrowPrefab, pos, Quaternion.identity);
                arrowSprites.Add(arrow.GetComponent<SwitchSprite>());
                arrow.transform.SetParent(canvas.transform);
            }
        }

        //�\���X�V
        ViewSprite();
    }

    void ViewSprite()
    {
        int loopCounter = 0;
        foreach (SwitchSprite s in arrowSprites)
        {
            if (counter + loopCounter < arrowsList.Count)
            {
                s.SwitchSpriteTo(arrowsList[counter + loopCounter]);
            }
            else
            {
                s.SwitchSpriteTo(KeyCode.Space);
            }
            loopCounter++;
        }
    } 

    void InterruptTask()
    {
        Debug.Log("�Q�[���𒆒f���܂���");
        isPlaying = false;
    }
}
