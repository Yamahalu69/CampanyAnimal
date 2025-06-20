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
    public Material stockMaterial;
    [Tooltip("�O��")]
    public Color displayColor;
    public Material displayMaterial;
    [Tooltip("���|")]
    public Color cleanColor;
    public Material cleanMaterial;
    [Tooltip("���W")]
    public Color registerColor;
    public Material registerMaterial;

    [Header("�^�X�N�ē��v���n�u")]
    public GameObject taskGuidePrefab;

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
    }

    /// <summary>
    /// ���ׂẴ^�X�N������
    /// </summary>
    public void CompletedTask()
    {

    }

    void AddTask(Task t)
    {
        Vector3 pos = Vector3.zero;
        GameObject taskGO = Instantiate(taskGuidePrefab, pos, Quaternion.identity);

        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);

        sensorText.Add(taskGO, rtText);

        rtText.transform.SetParent(remainTaskTextPT);

        switch (t)
        {
            case Task.display:
                taskGO.tag = "display";
                taskGO.GetComponent<MeshRenderer>().material = displayMaterial;
                pos = dispGuidePos[Random.Range(0, dispGuidePos.Count)].position;
                rtText.GetComponent<Text>().color = displayColor;
                break;
            case Task.register:
                taskGO.tag = "register";
                taskGO.GetComponent<MeshRenderer>().material = registerMaterial;
                pos = registerGuidePos.position;
                rtText.GetComponent<Text>().color = registerColor;
                break;
            case Task.stocking:
                taskGO.tag = "stocking";
                taskGO.GetComponent<MeshRenderer>().material = stockMaterial;
                pos = stockGuidePos[Random.Range(0, stockGuidePos.Count)].position;
                rtText.GetComponent<Text>().color = stockColor;
                break;
            case Task.cleaning:
                taskGO.tag = "cleaning";
                taskGO.GetComponent<MeshRenderer>().material = cleanMaterial;
                pos = cleanGuidePos[Random.Range(0, cleanGuidePos.Count)].position;
                rtText.GetComponent<Text>().color = cleanColor;
                break;
        }

        taskGO.transform.position = pos;
    }

    public void DeleteSensor(GameObject taskGO)
    {
        Destroy(sensorText[taskGO]);
        sensorText.Remove(taskGO);
        Destroy(taskGO);

        if (sensorText.Count == 0)
        {
            GameManager.instance.GameClear();
        }
    }
}
