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
        if (Input.GetKey(KeyCode.Return))//����̃L�[������@���̃L�[����͂���ƃV�[�����ړ�����
        {
            SceneManager.LoadScene("TitleScene");//�V�[����������
        }
    }
}
