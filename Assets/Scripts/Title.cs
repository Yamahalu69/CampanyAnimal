using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//特定のキーを入れる　そのキーを入力するとシーンが移動する
        {
            SceneManager.LoadScene("TitleScene");//シーン名を入れる
        }
    }
}
