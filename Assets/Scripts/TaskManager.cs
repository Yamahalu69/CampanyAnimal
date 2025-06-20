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
    [Header("色")]
    [Tooltip("入荷")]
    public Color stockColor;
    public Material stockMaterial;
    [Tooltip("前陳")]
    public Color displayColor;
    public Material displayMaterial;
    [Tooltip("清掃")]
    public Color cleanColor;
    public Material cleanMaterial;
    [Tooltip("レジ")]
    public Color registerColor;
    public Material registerMaterial;

    [Header("タスク案内プレハブ")]
    public GameObject taskGuidePrefab;

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
    }

    /// <summary>
    /// すべてのタスクが完了
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
