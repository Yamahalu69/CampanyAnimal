using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float sp;//�v���C���[�̃X�s�[�h
    private bool pl = true;//�v���C���[���^�X�N���s���Ă��邩
    [SerializeField] private GameObject cleantask;//���|�^�X�N
    private bool csencer = false;//���|�^�X�N�̕\���Ɣ�\���Ɏg�p
    [SerializeField] private GameObject displaytask;//�O�^�X�N
    private bool dsencer = false;//�O�^�X�N�̕\���Ɣ�\���Ɏg�p
    [SerializeField] RegisterTask registerTask;//���W�ł��^�X�N
    private bool rsencer = false;//���W�ł��^�X�N�̕\���Ɣ�\��
    [SerializeField] StockingTask stockingTask;//���׃^�X�N
    private bool ssencer = false;//���׃^�X�N�̕\���Ɣ�\��


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

        Register();//���W�ł��^�X�N

        Stocking();//���׃^�X�N
        
        //�Q�[���I��
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
        
    }

    private void OnTriggerEnter�@(Collider other)
    {
        if (other.gameObject.CompareTag("cleaning"))
        {
            csencer = true;
        }     
        else if (other.gameObject.CompareTag("display"))
        {
            Debug.Log("aaa");
            dsencer = true;
        }
        else if(other.gameObject.CompareTag("register"))
        {
            rsencer = true;
        }
        else if(other.gameObject.CompareTag("stocking"))
        {
            ssencer = true;
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cleaning"))
        {
            csencer = false;
        }
        else if (other.gameObject.CompareTag("display"))
        {
            dsencer = false;
        }
        else if (other.gameObject.CompareTag("register"))
        {
            rsencer = false;
        }
        else if (other.gameObject.CompareTag("stocking"))
        {
            ssencer = false;
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
        //�^�X�N����
        if(cleantask == null)
        {
            pl = true;
        }
    }
    private void DisplayTask()
    {
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return) && dsencer == true)
        {
            Debug.Log("bbb");
            displaytask.SetActive(true);
            pl = false;
        }
        //�^�X�N�I��
        else if (Input.GetKeyDown(KeyCode.Space) && dsencer == true) 
        {
            displaytask.SetActive(false);
            pl = true;
        }
    }
   
    private void Register()
    {
        if (Input.GetKeyDown(KeyCode.Return)&&rsencer==true)
        {
            registerTask.StartTask();
            pl = false;
        }

        if(Input.GetKeyDown(KeyCode.Space)&&rsencer==true)
        {
            pl= true;
        }
    }

    private void Stocking()
    {
        //�^�X�N�J�n
        if (Input.GetKeyDown(KeyCode.Return)&&ssencer==true)
        {
            stockingTask.StartTask();
            pl = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)==true)
        {
            pl= true;
        }
    }

}
