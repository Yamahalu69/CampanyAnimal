using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UnityEngine.Analytics;
using NUnit.Framework;

public class RegisterTask : MonoBehaviour
{
    [Header("�P�̋N���p��")]
    public bool isDebug;

    public bool isPlaying;

    [Tooltip("KeyCode��List")]
    List<KeyCode> arrowsList = new List<KeyCode>();

    //[Header("�^�X�N�����ɕK�v�Ȗ����͉�")]
    //public int arrowsAmount;

    [Header("���p�^�[�����X�g(ArrowList)")]
    public List<ArrowList> arrowLists;

    [Header("�X����b���e")]
    public List<string> clerkTalks;

    [Header("�q��b���e")]
    public List<string> customerTalks;

    [Header("�����o���I�u�W�F�N�g")]
    public GameObject talkPrefab;

    //�����o���I�u�W�F�N�g���X�g
    private List<GameObject> talkList = new List<GameObject>();

    [Header("�X�������o���\���ʒu")]
    public Transform clerkTransform;

    [Header("�q�����o���\���ʒu")]
    public Transform customerTransform;

    [Header("�����o���㏸��")]
    public float talkRise;

    [Header("�����o���ړ�����")]
    public float animationTime;

    private int counter = 0;

    [Header("���\���p�L�����o�X")]
    public GameObject canvas;

    [Header("���\���I�u�W�F�N�g")]
    public GameObject arrowPrefab;

    [Header("���c�萔�\���I�u�W�F�N�g")]
    public GameObject arrowRemainObject;

    [Header("���\���̈ʒu")]
    public Transform[] transforms = new Transform[4];

    [Header("���c�萔�\���̈ʒu")]
    public Transform remainTransform;

    [Header("���\���̑傫��(�{��)")]
    public float[] size = new float[4];

    private List<SwitchSprite> arrowSprites = new List<SwitchSprite>();

    private List<GameObject> instantObject = new List<GameObject>();

    private Text remainText;

    private int arrowRemain;

    [SerializeField] Player player;

    void Start()
    {
        if (isDebug) StartTask();
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
            //�A�j���[�V�������̏ꍇ�A�j���[�V�����X�L�b�v
            StopAnimation();

            if (Input.GetKeyDown(arrowsList[counter]))
            {
                //����������

                //�J�E���^�[����
                counter++;

                //�����o���f�n����
                CreateConversation();

                //�A�j���[�V����
                StartAnimation();

                //�^�X�N��������
                if (counter == arrowsList.Count)
                {
                    //�^�X�N����
                    Debug.Log("�^�X�N����");
                    StopTask();
                    player.taskfinish = true;
                }

                //�\���X�V
                View();
            }
            else
            {
                //�Ԉ��������
                //�ăX�^�[�g
                StopAnimation();
                StartTask();
            }
        }
    }

    private void StartAnimation()
    {
        foreach (GameObject talkGO in talkList)
        {
            Vector3 pos = new Vector3(0, talkRise, 0) + talkGO.transform.position;
            talkGO.transform.DOMove(pos, animationTime);
        }
    }

    private void StopAnimation()
    {
        if (talkList.Count == 0) return;
        foreach (GameObject talkGO in talkList)
        {
            talkGO.transform.DOComplete();
        }
    }

    public void StartTask()
    {
        //����������
        isPlaying = true;
        counter = 0;

        //�����o���I�u�W�F�N�g������Ȃ�폜
        if (talkList.Count != 0)
        {
            foreach (GameObject go in talkList)
            {
                Destroy(go);
            }
            talkList.Clear();
        }

        //���p�^�[������KeyCode�𐶐�
        SetPatternArrow();

        //���\���p�I�u�W�F�N�g����
        CreateArrowObject();

        //���c�萔�\���I�u�W�F�N�g����
        if (GameObject.Find("ArrowRemain(Clone)") == null)
        {
            GameObject remainObject = Instantiate(arrowRemainObject, remainTransform.position, Quaternion.identity);
            remainText = GameObject.Find("ArrowRemainText").GetComponent<Text>();
            remainObject.transform.SetParent(canvas.transform);
            remainObject.transform.position = remainTransform.position;
            instantObject.Add(remainObject);
        }

        //�\���X�V
        View();
    }

    void StopTask()
    {

        foreach (GameObject o in instantObject.ToList())
        {
            Destroy(o);
        }
        instantObject.Clear();
    }

    void CreateArrowObject()
    {
        if (arrowSprites.Count == 0)
        {
            //���\���p�I�u�W�F�N�g����
            for (int i = 0; i < 4; i++)
            {
                //Vector3 pos = new(1510f - 450 * i, 200f, 0f);
                Vector3 pos = transforms[i].position;

                GameObject arrow = Instantiate(arrowPrefab, Vector2.zero, Quaternion.identity);
                arrowSprites.Add(arrow.GetComponent<SwitchSprite>());
                arrow.transform.SetParent(canvas.transform);
                arrow.transform.position = pos;
                arrow.transform.localScale *= size[i];
                instantObject.Add(arrow);
            }
        }
    }

    void View()
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
        arrowRemain = arrowsList.Count - counter;
        remainText.text = arrowRemain.ToString() + "��";
    }

    void CreateConversation()
    {
        GameObject clarkTalkGO = Instantiate(talkPrefab, clerkTransform.position, Quaternion.identity);
        Text clarkTalkText = clarkTalkGO.GetComponentInChildren<Text>();
        clarkTalkGO.transform.SetParent(canvas.transform);
        clarkTalkText.text = clerkTalks[Random.Range(0, clerkTalks.Count - 1)];
        talkList.Add(clarkTalkGO);
        instantObject.Add(clarkTalkGO);

        GameObject customerTalkGO = Instantiate(talkPrefab, customerTransform.position, Quaternion.identity);
        Text customerTalkText = customerTalkGO.GetComponentInChildren<Text>();
        customerTalkGO.transform.SetParent(canvas.transform);
        customerTalkText.text = customerTalks[Random.Range(0, customerTalks.Count - 1)];
        talkList.Add(customerTalkGO);
        instantObject.Add(customerTalkGO);
    }

    void SetPatternArrow()
    {
        arrowsList.Clear();
        Random.InitState(System.DateTime.Now.Millisecond);
        List<Arrow> arrows = arrowLists[Random.Range(0, arrowLists.Count)].arrowList;
        
        foreach (Arrow arrow in arrows)
        {
            switch (arrow)
            {
                case Arrow.Up:
                    arrowsList.Add(KeyCode.UpArrow);
                    break;
                case Arrow.Down:
                    arrowsList.Add(KeyCode.DownArrow);
                    break;
                case Arrow.Right:
                    arrowsList.Add(KeyCode.RightArrow);
                    break;
                case Arrow.Left:
                    arrowsList.Add(KeyCode.LeftArrow);
                    break;
            }
        }
    }

    /*void SetRandamArrow()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
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
    }*/

    void InterruptTask()
    {
        Debug.Log("�Q�[���𒆒f���܂���");
        StopTask();
        isPlaying = false;
    }
}
