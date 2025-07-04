using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TaskManager taskManager;

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
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        GameStart();
    }

    void GameStart()
    {
        taskManager.Init();
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
