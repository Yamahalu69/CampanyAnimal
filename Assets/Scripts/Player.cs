using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float sp;//�v���C���[�̃X�s�[�h

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movex = 0;
        float movez = 0;
        //�l�����ړ�
        if (Input.GetKey(KeyCode.W))
        {
            movez = sp;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movez = -sp;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movex = sp;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movex = -sp;
        }

        transform.Translate(new Vector3(movex, 0, movez));
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enter");
        }
        //�^�X�N���f
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
        }
        
    }
    private void FixedUpdate()
    {
        
        
    }
   
}
