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
    }

    void GameStart()
    {
        taskManager.Init();
    }

    public void GameClear()
    {
        Debug.Log("GameClear");
        SceneManager.LoadScene("GameClearScene");
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene("GameOverScene");
    }
}
