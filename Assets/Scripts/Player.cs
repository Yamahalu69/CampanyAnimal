using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float sp;//�v���C���[�̃X�s�[�h
    private bool pl = true;//�v���C���[���^�X�N���s���Ă��邩
    [SerializeField] private GameObject cleantask;//���|�^�X�N
    private bool csencer;//���|�^�X�N�̕\���Ɣ�\���Ɏg�p
    //[SerializeField] private GameObject atask;//�e�X�g�^�X�N
    private bool tsencer;//�e�X�g�^�X�N�̕\���Ɣ�\���Ɏg�p

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cleantask.SetActive(false);
       // atask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        Playermove();//�v���C���[�̈ړ�����

        CleanTask();//�|���^�X�N�J�n�ƏI��

        ATask();//ATask�J�n�ƏI��
        
        
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
            tsencer = true;
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
            tsencer = false;
        }
    }

    private void Playermove()
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
    }

    private void CleanTask()
    {
         
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return) && csencer == true) 
        {
            cleantask.SetActive(true);
        }
        //�^�X�N���f
        if (Input.GetKeyDown(KeyCode.Space) && csencer == true)
        {
            cleantask.SetActive(false);
        }
    }
    private void ATask()
    {
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return) && tsencer == true)
        {
            //atask.SetActive(true);
        }
        //�^�X�N�I��
        if (Input.GetKeyDown(KeyCode.Space) && tsencer == true) 
        {
            //atask.SetActive(false);
        }
    }

}
