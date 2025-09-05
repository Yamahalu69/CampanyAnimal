using UnityEngine;

[CreateAssetMenu(fileName = "TimeEvent", menuName = "Scriptable Objects/TimeEvent")]
public class TimeEvent : ScriptableObject
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
    //public TimeEvent(float triggerT, EventList @event)
    //{
    //    triggerTime = triggerT;
    //    triggered = false;
    //    actionEvent = @event;
    //}
    public float triggerTime;
    [HideInInspector]
    public bool triggered = false;
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
        AudioManager.instance.WarningSE();
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
