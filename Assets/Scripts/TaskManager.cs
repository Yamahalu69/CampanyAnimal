using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public enum Task
{
    display,
    stocking,
    register,
    cleaning,
}

public class TaskManager : MonoBehaviour
{
    [Header("ゲージ")]
    public GageManager gageManager;

    [Header("NPC")]
    public NewNPCManager newNPCManager;

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

    [Header("ランダムで発生する前陳タスクの総数")]
    public int randomDipsTaskCount;

    [Header("残りタスクテキスト親transform")]
    public Transform remainTaskTextPT;

    [Header("前陳タスク案内の座標")]
    public List<Transform> dispGuidePos = new List<Transform>();

    [Header("入荷タスク案内の座標")]
    public List<Transform> stockGuidePos = new List<Transform>();

    [Header("清掃タスク案内の座標")]
    public List<Transform> cleanGuidePos = new List<Transform>();

    [Header("レジ打ちタスク案内の座標")]
    public Transform registerGuidePos;

    //private int dispTaskIndex = 0;
    //private int stockTaskIndex = 0;
    //private int cleanTaskIndex = 0;

    private List<int> dispIndexes = new List<int>();
    private List<int> stockIndexes = new List<int>();
    private List<int> cleanIndexes = new List<int>();

    private Dictionary<GameObject, int> taskGOToIndex = new Dictionary<GameObject, int>();

    private HashSet<int> usedDispIndexes = new HashSet<int>();
    private HashSet<int> usedStockIndexes = new HashSet<int>();
    private HashSet<int> usedCleanIndexes = new HashSet<int>();


    private Dictionary<GameObject, GameObject> sensorText = new Dictionary<GameObject, GameObject>();

    public int sensorTextCount => sensorText.Count;

    void Start()
    {

    }

    public void Init()
    {
        dispIndexes = Enumerable.Range(0, dispGuidePos.Count).OrderBy(a => Guid.NewGuid()).ToList();
        stockIndexes = Enumerable.Range(0, stockGuidePos.Count).OrderBy(a => Guid.NewGuid()).ToList();
        cleanIndexes = Enumerable.Range(0, cleanGuidePos.Count).OrderBy(a => Guid.NewGuid()).ToList();


        for (int i = 0; i < displayTaskCount; i++)
        {
            AddTask(Task.display);
        }

        AddTask(Task.register);
        AddTask(Task.cleaning);
        AddTask(Task.display);
        AddTask(Task.stocking);

    }

    public List<GameObject> TaskGOs()
    {
        return sensorText.Keys.ToList();
    }

    /// <summary>
    /// すべてのタスクが完了
    /// </summary>
    public void CompletedTask()
    {
        //GameManager.instance.GameClear();
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
        int index;
        Vector3 pos = TaskPos(Task.display, out index);
        if (pos == Vector3.zero) return;
        GameObject taskGO = Instantiate(displayTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "display";
        rtText.GetComponent<Text>().color = displayColor;
        rtText.GetComponent<Text>().text = "前陳";
        sensorText.Add(taskGO, rtText);
        taskGOToIndex[taskGO] = index;
    }

    void CreateRegiTaskGO()
    {
        int index;
        Vector3 pos = TaskPos(Task.register, out index);
        if (pos == Vector3.zero) return;
        GameObject taskGO = Instantiate(registerTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "register";
        rtText.GetComponent<Text>().color = registerColor;
        rtText.GetComponent<Text>().text = "レジ打ち";
        sensorText.Add(taskGO, rtText);
        gageManager.targetObject = taskGO;
        newNPCManager.targetObject = taskGO;
    }

    void CreateStockTaskGO()
    {
        int index;
        Vector3 pos = TaskPos(Task.stocking, out index);
        if (pos == Vector3.zero) return;
        GameObject taskGO = Instantiate(stockTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "stocking";
        rtText.GetComponent<Text>().color = stockColor;
        rtText.GetComponent<Text>().text = "入荷";
        sensorText.Add(taskGO, rtText);
        taskGOToIndex[taskGO] = index;
    }

    void CreateCleanTaskGO()
    {
        int index;
        Vector3 pos = TaskPos(Task.cleaning, out index);
        if (pos == Vector3.zero) return;
        GameObject taskGO = Instantiate(cleanTaskGuidePrefab, pos, Quaternion.identity);
        GameObject rtText = Instantiate(remainTaskText, Vector3.zero, Quaternion.identity);
        rtText.transform.SetParent(remainTaskTextPT);
        taskGO.tag = "cleaning";
        rtText.GetComponent<Text>().color = cleanColor;
        rtText.GetComponent<Text>().text = "清掃";
        sensorText.Add(taskGO, rtText);
        taskGOToIndex[taskGO] = index;
    }

    public void DeleteSensor(GameObject taskGO)
    {
        StartCoroutine(sensorText[taskGO].GetComponent<TaskTextAnimation>().EraseTextAnim());

        if (taskGOToIndex.TryGetValue(taskGO, out int index))
        {
            string tag = taskGO.tag;
            switch (tag)
            {
                case "display":
                    usedDispIndexes.Remove(index);
                    break;
                case "stocking":
                    usedStockIndexes.Remove(index);
                    break;
                case "cleaning":
                    usedCleanIndexes.Remove(index);
                    break;
            }
            taskGOToIndex.Remove(taskGO);
        }

        sensorText.Remove(taskGO);
        Destroy(taskGO);

        if (sensorText.Count == 0)
        {
            CompletedTask();
        }
    }

    Vector3 TaskPos(Task task, out int index)
    {
        index = -1;
        //posをリストから順番に参照して返す
        switch (task)
        {
            case Task.display:
                for (int i = 0; i < dispGuidePos.Count; i++)
                {
                    if (!usedDispIndexes.Contains(i))
                    {
                        usedDispIndexes.Add(i);
                        index = i;
                        return dispGuidePos[i].position;
                    }
                }
                break;

            case Task.register:
                index = 0;
                return registerGuidePos.position;

            case Task.stocking:
                for (int i = 0; i < stockGuidePos.Count; i++)
                {
                    if (!usedStockIndexes.Contains(i))
                    {
                        usedStockIndexes.Add(i);
                        index = i;
                        return stockGuidePos[i].position;
                    }
                }
                break;

            case Task.cleaning:
                for (int i = 0; i < cleanGuidePos.Count; i++)
                {
                    if (!usedCleanIndexes.Contains(i))
                    {
                        usedCleanIndexes.Add(i);
                        index = i;
                        return cleanGuidePos[i].position;
                    }
                }
                break;
        }
        return Vector3.zero;
    }
}
