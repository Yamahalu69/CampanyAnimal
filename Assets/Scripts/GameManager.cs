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
            StopRandomSpawn
        }
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

        public void StopRandomSpawn()
        {
            //GameManager.instance.boolSpawn = false;
        }
    }

    public static GameManager instance;

    public TaskManager taskManager;

    private float timer;

    private List<TimeEvent> events = new List<TimeEvent>()
    {
        new TimeEvent(10, TimeEvent.EventList.CustomerRaid),
        new TimeEvent(20, TimeEvent.EventList.AddDisplayTask),
        new TimeEvent(30, TimeEvent.EventList.AddStockTask),
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
    }

    public void GameClear()
    {
        SceneManager.LoadScene("GameClearScene");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void OverWorkGameOver()
    {
        SceneManager.LoadScene("GameOverSceneOverWork");
    }
}
