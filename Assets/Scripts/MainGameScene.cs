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
        if (Input.GetKey(KeyCode.LeftShift))//����̃L�[������@���̃L�[����͂���ƃV�[�����ړ�����
        {
            AudioManager.instance.PlayingBGM();
            SceneManager.LoadScene("MainGameScene");//�V�[����������
        }
        else if (Input.GetKey(KeyCode.RightShift))//����̃L�[������@���̃L�[����͂���ƃV�[�����ړ�����
        {
            AudioManager.instance.PlayingBGM();
            SceneManager.LoadScene("MainGameScene");//�V�[����������
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Operation");
        }
    }
}

