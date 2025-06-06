using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float sp;//�v���C���[�̃X�s�[�h
    private bool pl = true;//�v���C���[���^�X�N���s���Ă��邩
    [SerializeField] private GameObject cleantask;//���|�^�X�N
    private bool csencer;//���|�^�X�N�̕\���Ɣ�\���Ɏg�p
    [SerializeField] private GameObject displaytask;//�O�^�X�N
    private bool dsencer;//�O�^�X�N�̕\���Ɣ�\���Ɏg�p

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pl= true;
        cleantask.SetActive(false);
        displaytask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        Playermove();//�v���C���[�̈ړ�����
        
        CleanTask();//�|���^�X�N�J�n�ƏI��

        DisplayTask();//��^�X�N�J�n�ƏI��
        
        
        //�Q�[���I��
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("clean"))
        {
            csencer = true;
        }
        else if(collision.gameObject.CompareTag("task"))
        {
            dsencer = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("clean"))
        {
            csencer = false; 
        }
        else if (collision.gameObject.CompareTag("task"))
        {
            dsencer = false;
        }
    }

    private void Playermove()
    {
        Vector3 vector =new Vector3(0,0,0);
        if(pl==true)
        {
            //�l�����ړ�
            if (Input.GetKey(KeyCode.W))
            {
                vector.z = 1.0f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                vector.z = -1.0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                vector.x = 1.0f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                vector.x = -1.0f;
            }
        }

        float length = Mathf.Sqrt((vector.x * vector.x) + (vector.z * vector.z));

        if ((0 < length))
        {
            vector = vector / length;
            vector *= sp;
            transform.position += vector;
        }
    }

    private void CleanTask()
    {
         
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return) && csencer == true) 
        {
            cleantask.SetActive(true);
            pl = false;
        }
        //�^�X�N���f
        if (Input.GetKeyDown(KeyCode.Space) && csencer == true)
        {
            cleantask.SetActive(false);
            pl = true;
        }
    }
    private void DisplayTask()
    {
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return) && dsencer == true)
        {
            displaytask.SetActive(true);
            pl= false;
        }
        //�^�X�N�I��
        if (Input.GetKeyDown(KeyCode.Space) && dsencer == true) 
        {
            displaytask.SetActive(false);
            pl = true;
        }
    }

}
