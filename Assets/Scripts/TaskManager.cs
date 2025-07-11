using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public enum Task
{
    display,
    stocking,
    register,
    cleaning,
}

public class TaskManager : MonoBehaviour
{
    [Header("�F")]
    [Tooltip("����")]
    public Color stockColor;
    [Tooltip("�O��")]
    public Color displayColor;
    [Tooltip("���|")]
    public Color cleanColor;
    [Tooltip("���W")]
    public Color registerColor;

    [Header("�^�X�N�ē��v���n�u")]
    public GameObject stockTaskGuidePrefab;
    public GameObject displayTaskGuidePrefab;
    public GameObject cleanTaskGuidePrefab;
    public GameObject registerTaskGuidePrefab;

    [Header("�c��^�X�N�e�L�X�g�v���n�u")]
    public GameObject remainTaskText;

    [Header("�����̑O�^�X�N�̐�")]
    public int displayTaskCount;

    [Header("�c��^�X�N�e�L�X�g�etransform")]
    public Transform remainTaskTextPT;

    [Header("�O�^�X�N�ē��̍��W")]
    public List<Transform> dispGuidePos = new List<Transform>();

    [Header("���׃^�X�N�ē��̍��W")]
    public List <Transform> stockGuidePos = new List<Transform>();

    [Header("���|�^�X�N�ē��̍��W")]
    public List<Transform> cleanGuidePos = new List<Transform>();

    [Header("���W�ł��^�X�N�ē��̍��W")]
    public Transform registerGuidePos;

    private int dispTaskIndex = 0;
    private int stockTaskIndex = 0;
    private int cleanTaskIndex = 0;

    private Dictionary<GameObject, GameObject> sensorText = new Dictionary<GameObject, GameObject>();

    void Start()
    {

    }

    public void Init()
    {
        for(int i = 0; i < displayTaskCount; i++)
        {
            AddTask(Task.display);
        }

        if (GameObject.Find("AgryGage")) Debug.Log("aaaa");

        AddTask(Task.cleaning);
        AddTask(Task.register);
        AddTask(Task.stocking);
    }

    /// <summary>
    /// ���ׂẴ^�X�N������
    /// </summary>
    public void CompletedTask()
    {
        GameManager.instance.GameClear();
    }

    public void AddTask(Task t)
    {
        switch (t)
        {
            case Task.display:
                CreateDispTaskGO();
                break;
            case Task.register:
                CreateRegiTaskGO();
                break;
            case Task.stocking:
                CreateStockTaskGO();
                break;
            case Task.cleaning:
                CreateCleanTaskGO();
                break;
        }
    }

    void CreateDispTaskGO()
    {
        Vector3 pos = TaskPos(Task.display);
        GameObject taskGO = Instantiate(displayTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "display";
        rtText.GetComponent<Text>().color = displayColor;
        rtText.GetComponent<Text>().text = "�O��";
        sensorText.Add(taskGO, rtText);
    }

    void CreateRegiTaskGO()
    {
        Vector3 pos = TaskPos(Task.register);
        GameObject taskGO = Instantiate(registerTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "register";
        rtText.GetComponent<Text>().color = registerColor;
        rtText.GetComponent<Text>().text = "���W�ł�";
        sensorText.Add(taskGO, rtText);
        GameObject.Find("AgryGage").GetComponent<GageManager>().targetObject = taskGO;
    }

    void CreateStockTaskGO()
    {
        Vector3 pos = TaskPos(Task.stocking);
        GameObject taskGO = Instantiate(stockTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "stocking";
        rtText.GetComponent<Text>().color = stockColor;
        rtText.GetComponent<Text>().text = "����";
        sensorText.Add(taskGO, rtText);
    }

    void CreateCleanTaskGO()
    {
        Vector3 pos = TaskPos(Task.cleaning);
        GameObject taskGO = Instantiate(cleanTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "cleaning";
        rtText.GetComponent<Text>().color = cleanColor;
        rtText.GetComponent<Text>().text = "���|";
        sensorText.Add(taskGO, rtText);
    }

    public void DeleteSensor(GameObject taskGO)
    {
        StartCoroutine(sensorText[taskGO].GetComponent<TaskTextAnimation>().EraseTextAnim());
        sensorText.Remove(taskGO);
        Destroy(taskGO);

        if (sensorText.Count == 0)
        {
            CompletedTask();
        }
    }

    Vector3 TaskPos(Task task)
    {
        //pos�����X�g���珇�ԂɎQ�Ƃ��ĕԂ�
        Vector3 pos = Vector3.zero;
        switch (task)
        {
            case Task.display:
                pos = dispGuidePos[dispTaskIndex].position;
                dispTaskIndex = (dispTaskIndex + 1) % dispGuidePos.Count;
                return pos;

            case Task.register:
                pos = registerGuidePos.position;
                return pos;

            case Task.stocking:
                pos = stockGuidePos[stockTaskIndex].position;
                stockTaskIndex = (stockTaskIndex + 1) % stockGuidePos.Count;
                return pos;

            case Task.cleaning:
                pos = cleanGuidePos[cleanTaskIndex].position;
                cleanTaskIndex = (cleanTaskIndex + 1) % cleanGuidePos.Count;
                return pos;
            default:
                return pos;
        }
    }
}
