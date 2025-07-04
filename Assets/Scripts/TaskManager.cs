using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

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

    private Dictionary<GameObject, GameObject> sensorText = new Dictionary<GameObject, GameObject>();
    private List<GameObject> taskGOs = new List<GameObject>();

    void Start()
    {

    }

    public void Init()
    {
        for(int i = 0; i < displayTaskCount; i++)
        {
            AddTask(Task.display);
        }

        AddTask(Task.cleaning);
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
        Vector3 pos = dispGuidePos[Random.Range(0, dispGuidePos.Count)].position;
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
        Vector3 pos = registerGuidePos.position;
        GameObject taskGO = Instantiate(registerTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "register";
        rtText.GetComponent<Text>().color = registerColor;
        rtText.GetComponent<Text>().text = "���W�ł�";
        sensorText.Add(taskGO, rtText);
    }

    void CreateStockTaskGO()
    {
        Vector3 pos = stockGuidePos[Random.Range(0, stockGuidePos.Count)].position;
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
        Vector3 pos = cleanGuidePos[Random.Range(0, cleanGuidePos.Count)].position;
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
}
