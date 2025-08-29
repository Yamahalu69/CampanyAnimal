using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StockingTask : MonoBehaviour
{
    [Header("�P�̋N���p��")]
    public bool isDebug;

    public bool isPlaying;

    [SerializeField]
    [Header("�^�X�N�̉�")]
    int task;

    [SerializeField]
    [Header("�o�[�̑���")]
    float speed;

    [SerializeField]
    [Header("�s���s�\����")]
    float stunTime;

    [SerializeField]
    [Header("�o�[�\���p�L�����o�X")]
    GameObject canvas;

    [Header("�T�C�h�o�[�I�u�W�F�N�g")]
    public GameObject sideBarPrefab;

    [Header("�T�C�h�o�[�𒆉�����ǂꂾ�����炷��")]
    public Vector2 offset;

    private GameObject barGO;

    private RectTransform sideBar;
    private RectTransform slider;
    private RectTransform target;

    private int counter;

    private bool isPlayable;
    private float timer;
    private bool goUp;

    private bool isEnteringFrame;

    private Text discText;

    [SerializeField] Player player;

    void Start()
    {
        if (isDebug) StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;
        if (isEnteringFrame)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                return;
            }
            else
            {
                isEnteringFrame = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isPlayable)
        {
            player.pl = true;
            InterruptTask();
        }

        if (Input.GetKeyDown(KeyCode.Return) && isPlayable)
        {
            AudioManager.instance.UIClick();
            //�o�[���d�Ȃ��Ă��邩����
            float upLimit = target.position.y + target.rect.height / 2f;
            float downLimit = target.position.y - target.rect.height / 2f;
            
            if (slider.position.y <= upLimit && slider.position.y >= downLimit)
            {
                //�͈͓�
                counter++;
                if (counter >= task)
                {
                    //�^�X�N����
                    //Debug.Log("�^�X�N����");
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
                //�͈͊O

                //�s���s�\
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
            //�X���C�_�[�̈ړ�
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
        //����������
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
        isEnteringFrame = true;
        counter = 0;

        discText = sideBar.GetComponentInChildren<Text>();
        StartAnimation();
    }

    void StopTask()
    {
        StopAnimation();
        Destroy(barGO);
    }

    void RandomMoveTarget()
    {
        float y = Random.Range(sideBar.rect.height / 2 - target.rect.height / 2, -sideBar.rect.height / 2 + target.rect.height / 2);

        target.transform.localPosition = new Vector2 (0, y);
    }

    void InterruptTask()
    {
        Debug.Log("�Q�[���𒆒f���܂���");
        StopTask();
        isPlaying = false;
    }

    void StartAnimation()
    {
        discText.DOColor(new Color(1f, 0, 0), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    void StopAnimation()
    {
        discText.DOKill();
    }
}
