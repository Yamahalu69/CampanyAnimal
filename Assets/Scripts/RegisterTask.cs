using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterTask : MonoBehaviour
{
    public bool isPlaying;

    List<KeyCode> arrowsList = new List<KeyCode>();

    public int arrowsAmount;

    private int counter = 0;

    //矢印表示用キャンバス
    public GameObject canvas;

    public GameObject arrowPrefab;

    public List<SwitchSprite> arrowSprites = new List<SwitchSprite>();

    void Start()
    {
        StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InterruptTask();
        }

        // 入力判定
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (Input.GetKeyDown(arrowsList[counter]))
            {
                //正しい入力
                counter++;
                if (counter == arrowsList.Count)
                {
                    //タスク完了
                    Debug.Log("タスク完了");
                }

                //表示更新
                ViewSprite();
            }
            else
            {
                //間違った入力
                StartTask();
            }
        }
    }

    public void StartTask()
    {
        //初期化処理
        Random.InitState(System.DateTime.Now.Millisecond);
        isPlaying = true;
        counter = 0;
        arrowsList.Clear();

        for (int i = 0; i < arrowsAmount; i++)
        {
            int r = Random.Range(1, 5);
            Debug.Log(r);
            switch (r)
            {
                case 1:
                    arrowsList.Add(KeyCode.UpArrow);
                    break;
                case 2:
                    arrowsList.Add(KeyCode.DownArrow);
                    break;
                case 3:
                    arrowsList.Add(KeyCode.RightArrow);
                    break;
                case 4:
                    arrowsList.Add(KeyCode.LeftArrow);
                    break;
            }
        }

        if (arrowSprites.Count == 0)
        {
            //矢印表示用オブジェクト生成
            for (int i = 0; i < 4; i++)
            {
                Vector3 pos = new(1510f - 450 * i, 200f, 0f);

                GameObject arrow = Instantiate(arrowPrefab, pos, Quaternion.identity);
                arrowSprites.Add(arrow.GetComponent<SwitchSprite>());
                arrow.transform.SetParent(canvas.transform);
            }
        }

        //表示更新
        ViewSprite();
    }

    void ViewSprite()
    {
        int loopCounter = 0;
        foreach (SwitchSprite s in arrowSprites)
        {
            if (counter + loopCounter < arrowsList.Count)
            {
                s.SwitchSpriteTo(arrowsList[counter + loopCounter]);
            }
            else
            {
                s.SwitchSpriteTo(KeyCode.Space);
            }
            loopCounter++;
        }
    } 

    void InterruptTask()
    {
        Debug.Log("ゲームを中断しました");
        isPlaying = false;
    }
}
