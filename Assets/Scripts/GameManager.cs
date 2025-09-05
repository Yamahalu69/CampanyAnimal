using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TaskManager taskManager;

    private float timer;
    private bool timeCount =false;

    public bool spawnTask = true;
    private float spawnTriggerTime = 0;
    public float randomSpawnMin = 0;
    public float randomSpawnMax = 10;

    List<float> spawnTimeList = new List<float>();
    private int spawnCount = 0;

    [SerializeField]
    private List<TimeEvent> events = new List<TimeEvent>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameStart();
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    void Update()
    {
        if (timeCount) timer += Time.deltaTime;
        foreach (var e in events)
        {
            if (!e.triggered && timer >= e.triggerTime)
            {
                e.ActionEvent();
            }
        }

        //ランダムタスクスポーン
        if (spawnTimeList[spawnCount] <= timer && spawnTask)
        {
            taskManager.AddTask(Task.display);
            if (spawnCount < spawnTimeList.Count - 1)
            {
                spawnCount++;
            }
        }
        //if (spawnTriggerTime <= timer && spawnTask)
        //{
        //    taskManager.AddTask(Task.display);
        //    spawnTriggerTime += Random.Range(randomSpawnMin, randomSpawnMax);
        //}
    }

    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        if (nextScene.name == "MainGameScene")
        {
            taskManager = GameObject.Find("Manager").GetComponent<TaskManager>();
            GameStart();
            timeCount = true;
        }
        else
        {
            timeCount = false;
        }
    }

    void GameStart()
    {
        taskManager.Init();
        timer = 0;
        spawnTask = true;
        spawnTriggerTime = 0;
        MakeSpawnTimes();
    }

    public void JudgeGameClear()
    {
        if (taskManager.sensorTextCount == 0)
        {
            GameClear();
        }
        else
        {
            GameOver();
        }
    }

    private void MakeSpawnTimes()
    {
        spawnTimeList.Clear();
        float stopTime = events.FirstOrDefault(e => e.actionEvent == TimeEvent.EventList.StopRandomSpawn)?.triggerTime ?? 0f;
        float maxTime = stopTime - randomSpawnMax;
        float multiplyFactor = maxTime / taskManager.randomDipsTaskCount;

        for (int i = 0; i < taskManager.randomDipsTaskCount; i++)
        {
            spawnTimeList.Add(multiplyFactor * (i + 1) + Random.Range(randomSpawnMin, randomSpawnMax));
        }
    }

    private void GameClear()
    {
        SceneManager.LoadScene("GameClearScene");
        AudioManager.instance.GameclearBGM();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        AudioManager.instance.GameoverBGM();
    }

    public void OverWorkGameOver()
    {
        SceneManager.LoadScene("GameOverSceneOverWork");
        AudioManager.instance.GameoverBGM();
    }
}
