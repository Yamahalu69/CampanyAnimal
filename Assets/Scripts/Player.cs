using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float sp;//プレイヤーのスピード
    private bool pl = true;//プレイヤーがタスクを行っているか
    [SerializeField] private GameObject cleantask;//清掃タスク
    private bool csencer;//清掃タスクの表示と非表示に使用
    //[SerializeField] private GameObject atask;//テストタスク
    private bool tsencer;//テストタスクの表示と非表示に使用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cleantask.SetActive(false);
       // atask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        Playermove();//プレイヤーの移動操作

        CleanTask();//掃除タスク開始と終了

        ATask();//ATask開始と終了
        
        
        //ゲーム終了
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
        //四方向移動
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
         
        //タスク開始
        if (Input.GetKeyDown(KeyCode.Return) && csencer == true) 
        {
            cleantask.SetActive(true);
        }
        //タスク中断
        if (Input.GetKeyDown(KeyCode.Space) && csencer == true)
        {
            cleantask.SetActive(false);
        }
    }
    private void ATask()
    {
        //タスク開始
        if (Input.GetKeyDown(KeyCode.Return) && tsencer == true)
        {
            //atask.SetActive(true);
        }
        //タスク終了
        if (Input.GetKeyDown(KeyCode.Space) && tsencer == true) 
        {
            //atask.SetActive(false);
        }
    }

}
