using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))//特定のキーを入れる　そのキーを入力するとシーンが移動する
        {
            AudioManager.instance.PlayingBGM();
            SceneManager.LoadScene("MainGameScene");//シーン名を入れる
        }
        else if (Input.GetKey(KeyCode.RightShift))//特定のキーを入れる　そのキーを入力するとシーンが移動する
        {
            AudioManager.instance.PlayingBGM();
            SceneManager.LoadScene("MainGameScene");//シーン名を入れる
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Operation");
        }
    }
}

