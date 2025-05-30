using UnityEngine;

public class StockingTask : MonoBehaviour
{
    public bool isPlaying;

    [SerializeField]
    [Tooltip("�^�X�N�̉�")]
    int task;

    [SerializeField]
    [Tooltip("�o�[�̑���")]
    float speed;

    [SerializeField]
    [Tooltip("�s���s�\����")]
    float stunTime;

    [SerializeField]
    [Tooltip("�o�[�\���p�L�����o�X")]
    GameObject canvas;

    public GameObject sideBarPrefab;

    private RectTransform sideBar;
    private RectTransform slider;
    private RectTransform target;

    private int counter;

    private bool isPlayable;
    private float timer;

    void Start()
    {
        StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.Escape) && isPlayable)
        {
            InterruptTask();
        }

        if (Input.GetKeyDown(KeyCode.Return) && isPlayable)
        {
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
                    Debug.Log("�^�X�N����");
                    isPlaying = false;
                }
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
            Debug.Log(timer);
            if (timer >= stunTime)
            {
                isPlayable = true;
            }
        }

        if (isPlayable && isPlaying)
        {
            //�X���C�_�[�̈ړ�
            float y = slider.localPosition.y + speed;
            slider.localPosition = new Vector2(0, y);
            if (slider.localPosition.y > sideBar.rect.height / 2)
            {
                slider.localPosition = new Vector2(0, -sideBar.rect.height / 2);
            }
        }
    }

    public void StartTask()
    {
        GameObject sideBarObject = Instantiate(sideBarPrefab, new(1560f, 540f), Quaternion.identity);
        sideBar = sideBarObject.GetComponent<RectTransform>();
        target = sideBar.Find("TargetPoint").gameObject.GetComponent<RectTransform>();
        slider = sideBar.Find("Slider").gameObject.GetComponent<RectTransform>();
        sideBar.SetParent(canvas.GetComponent<Transform>());

        isPlaying = true;
        isPlayable = true;
        counter = 0;
    }

    void InterruptTask()
    {
        Debug.Log("�Q�[���𒆒f���܂���");
        isPlaying = false;
    }
}
