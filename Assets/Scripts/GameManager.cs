using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TaskManager taskManager;

    private float timer; //�^�C�}�[�i�����5���͂���j
    private bool timeCount =false; //�Q�[�����̂݃J�E���g��i�߂�

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

        //�����_���^�X�N�X�|�[��
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
        RegisterTask rt = GameObject.Find("Manager").GetComponent<RegisterTask>();
        rt.StopTask();
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
        //�O�^�X�N�̔�������^�C�~���O�����X�g�ɂ��č쐬.
        //������.
        spawnTimeList.Clear();
        //null�`�F�b�N.
        float stopTime = events.FirstOrDefault(e => e.actionEvent == TimeEvent.EventList.StopRandomSpawn)?.triggerTime ?? 0f;
        //�Œx�ł��܂ŃX�|�[�����邩�v�Z.
        float maxTime = stopTime - randomSpawnMax;
        //�ő�X�|�[�����ŋϓ�����(������߂�).
        float multiplyFactor = maxTime / taskManager.randomDipsTaskCount;

        //��ɑ΂��Ē����������_���ɕω�.
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
        RegisterTask rt = GameObject.Find("Manager").GetComponent<RegisterTask>();
        rt.StopTask();
        SceneManager.LoadScene("GameOverSceneOverWork");
        AudioManager.instance.GameoverBGM();
    }
}
