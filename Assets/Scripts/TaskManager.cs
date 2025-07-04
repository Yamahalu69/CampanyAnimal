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
    [Header("色")]
    [Tooltip("入荷")]
    public Color stockColor;
    [Tooltip("前陳")]
    public Color displayColor;
    [Tooltip("清掃")]
    public Color cleanColor;
    [Tooltip("レジ")]
    public Color registerColor;

    [Header("タスク案内プレハブ")]
    public GameObject stockTaskGuidePrefab;
    public GameObject displayTaskGuidePrefab;
    public GameObject cleanTaskGuidePrefab;
    public GameObject registerTaskGuidePrefab;

    [Header("残りタスクテキストプレハブ")]
    public GameObject remainTaskText;

    [Header("初期の前陳タスクの数")]
    public int displayTaskCount;

    [Header("残りタスクテキスト親transform")]
    public Transform remainTaskTextPT;

    [Header("前陳タスク案内の座標")]
    public List<Transform> dispGuidePos = new List<Transform>();

    [Header("入荷タスク案内の座標")]
    public List <Transform> stockGuidePos = new List<Transform>();

    [Header("清掃タスク案内の座標")]
    public List<Transform> cleanGuidePos = new List<Transform>();

    [Header("レジ打ちタスク案内の座標")]
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
    /// すべてのタスクが完了
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
        rtText.GetComponent<Text>().text = "前陳";
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
        rtText.GetComponent<Text>().text = "レジ打ち";
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
        rtText.GetComponent<Text>().text = "入荷";
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
        rtText.GetComponent<Text>().text = "清掃";
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
