using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneyou : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))//特定のキーを入れる　そのキーを入力するとシーンが移動する
        {
            SceneManager.LoadScene("");//シーン名を入れる
        }
    }
}

