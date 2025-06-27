using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float sp;//プレイヤーのスピード
    private bool pl = true;//プレイヤーがタスクを行っているか
    [SerializeField] private GameObject cleantask;//清掃タスク
    private bool csencer = false;//清掃タスクの表示と非表示に使用
    [SerializeField] private GameObject displaytask;//前陳タスク
    private bool dsencer = false;//前陳タスクの表示と非表示に使用
    [SerializeField] RegisterTask registerTask;//レジ打ちタスク
    private bool rsencer = false;//レジ打ちタスクの表示と非表示
    [SerializeField] StockingTask stockingTask;//入荷タスク
    private bool ssencer = false;//入荷タスクの表示と非表示


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
       
        Playermove();//プレイヤーの移動操作
        
        CleanTask();//掃除タスク開始と終了

        DisplayTask();//陳列タスク開始と終了

        Register();//レジ打ちタスク

        Stocking();//入荷タスク
        
        //ゲーム終了
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
        
    }

    private void OnTriggerEnter　(Collider other)
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
            //四方向移動
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
         
        //タスク開始
        if (Input.GetKeyDown(KeyCode.Return) && csencer == true) 
        {
            cleantask.SetActive(true);
            pl = false;
        }
        //タスク中断
        if (Input.GetKeyDown(KeyCode.Space) && csencer == true)
        {
            cleantask.SetActive(false);
            pl = true;
        }
        //タスク完了
        if(cleantask == null)
        {
            pl = true;
        }
    }
    private void DisplayTask()
    {
        //タスク開始
        if (Input.GetKeyDown(KeyCode.Return) && dsencer == true)
        {
            Debug.Log("bbb");
            displaytask.SetActive(true);
            pl = false;
        }
        //タスク終了
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
        //タスク開始
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
