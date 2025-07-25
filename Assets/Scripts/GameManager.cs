using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private class TimeEvent
    {
        public enum EventList
        {
            CustomerRaid,
            AddStockTask,
            AddDisplayTask,
            AddCleanTask,
            StopRandomSpawn
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerT">イベント発生時間(経過時間)</param>
        /// <param name="event">発生させるイベント</param>
        public TimeEvent(float triggerT, EventList @event)
        {
            triggerTime = triggerT;
            triggered = false;
            actionEvent = @event;
        }
        public float triggerTime;
        public bool triggered;
        public EventList actionEvent;

        public void ActionEvent()
        {
            triggered = true;
            switch (actionEvent)
            {
                case EventList.CustomerRaid:
                    CustomerRaid();
                    break;
                case EventList.AddStockTask:
                    AddStockTask();
                    break;
                case EventList.AddDisplayTask:
                    AddDisplayTask();
                    break;
                case EventList.AddCleanTask:
                    AddCleanTask();
                    break;
                case EventList.StopRandomSpawn:
                    StopRandomSpawn();
                    break;
            }
        }

        public void CustomerRaid()
        {
            GameManager.instance.taskManager.AddTask(Task.register);
            //NPC
        }

        public void AddStockTask()
        {
            GameManager.instance.taskManager.AddTask(Task.stocking);
        }

        public void AddDisplayTask()
        {
            GameManager.instance.taskManager.AddTask(Task.display);
        }

        public void AddCleanTask()
        {
            GameManager.instance.taskManager.AddTask(Task.cleaning);
        }

        public void StopRandomSpawn()
        {
            GameManager.instance.spawnTask = false;
        }
    }

    public static GameManager instance;

    public TaskManager taskManager;

    private float timer;

    public bool spawnTask = true;
    private float spawnTriggerTime = 0;
    public float randomSpawnMin = 0;
    public float randomSpawnMax = 10;

    private List<TimeEvent> events = new List<TimeEvent>()
    {
        new TimeEvent(10, TimeEvent.EventList.CustomerRaid),
        new TimeEvent(20, TimeEvent.EventList.AddDisplayTask),
        new TimeEvent(30, TimeEvent.EventList.AddStockTask),
        new TimeEvent(50, TimeEvent.EventList.StopRandomSpawn),
    };

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
        timer += Time.deltaTime;
        foreach (var e in events)
        {
            if (!e.triggered && timer >= e.triggerTime)
            {
                e.ActionEvent();
            }
        }

        if (spawnTriggerTime <= timer && spawnTask)
        {
            taskManager.AddTask(Task.display);
            spawnTriggerTime += Random.Range(randomSpawnMin, randomSpawnMax);
        }
    }

    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        if (nextScene.name == "MainGameScene")
        {
            taskManager = GameObject.Find("Manager").GetComponent<TaskManager>();
            GameStart();
        }
    }

    void GameStart()
    {
        taskManager.Init();
        timer = 0;
        spawnTask = true;
        spawnTriggerTime = 0;
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

    private void GameClear()
    {
        SceneManager.LoadScene("GameClearScene");
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void OverWorkGameOver()
    {
        SceneManager.LoadScene("GameOverSceneOverWork");
    }
}
