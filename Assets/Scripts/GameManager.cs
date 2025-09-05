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

    public bool spawnTask = false;
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
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    void Update()
    {
        if (timeCount) timer += Time.deltaTime;
        foreach (var e in events)
        {
            if ((!e.triggered) && timer >= e.triggerTime)
            {
                e.ActionEvent();
            }
        }

        //ランダムタスクスポーン
        if (spawnTimeList.Count == 0) return;
        if (spawnTimeList[spawnCount] <= timer && spawnTask)
        {
            taskManager.AddTask(Task.display);
            if (spawnCount < spawnTimeList.Count - 1)
            {
                spawnCount++;
            }
            else if (spawnCount == spawnTimeList.Count - 1)
            {
                spawnTimeList.Clear();
            }
        }
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

        foreach (var e in events)
        {
            e.triggered = false;
        }

        timer = 0;
        spawnTask = true;
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
